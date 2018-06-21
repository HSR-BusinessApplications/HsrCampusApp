// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.Droid.Widgets
{
    using System;
    using Android.Content;
    using Android.Graphics;
    using Android.Util;
    using Android.Views;
    using Android.Widget;
    using Core.ViewModels;
    using Gestures;

    public delegate void ClickLocation(object sender, LocationEventArgs e);

    public class TouchImageView : ImageView
    {
        public const int ClickThreshold = 3;
        public const float MaxScale = 4.0f;
        public const float MinScale = 0.4f;

        private readonly Matrix mMatrix = new Matrix();
        private readonly PointF start = new PointF();

        private double mFocusX;
        private double mFocusY;
        private int mImageHeight;
        private int mImageWidth;
        private int mViewWidth;
        private int mViewHeight;

        private ScaleGestureDetector mScaleDetector;
        private MoveGestureDetector mMoveDetector;

        public TouchImageView(Context context)
            : base(context)
        {
            this.Init(context);
        }

        public TouchImageView(Context context, IAttributeSet attrs)
            : base(context, attrs)
        {
            this.Init(context);
        }

        public event ClickLocation ClickLoction;

        public double ScaleFactor { get; set; }

        public void InitialiseImage()
        {
            this.mMatrix.Reset();

            // Fill screen with image
            var scaleX = this.mViewWidth / (double)this.mImageWidth;
            var scaleY = this.mViewHeight / (double)this.mImageHeight;
            this.ScaleFactor = Math.Min(scaleX, scaleY);
            this.mMatrix.SetScale((float)this.ScaleFactor, (float)this.ScaleFactor);

            // Center the image
            var redundantYSpace = this.mViewHeight - (this.ScaleFactor * this.mImageHeight);
            var redundantXSpace = this.mViewWidth - (this.ScaleFactor * this.mImageWidth);
            redundantYSpace /= 2;
            redundantXSpace /= 2;
            this.mMatrix.PostTranslate((float)redundantXSpace, (float)redundantYSpace);

            // set initial focus values (otherwise jumps on first touch)
            this.mFocusX = redundantXSpace + this.GetScaledImageCenterX();
            this.mFocusY = redundantYSpace + this.GetScaledImageCenterY();

            this.mMatrix.PostRotate(0);

            this.ImageMatrix = this.mMatrix;
        }

        public override bool OnTouchEvent(MotionEvent e)
        {
            this.mScaleDetector.OnTouchEvent(e);
            this.mMoveDetector.OnTouchEvent(e);

            var scaledImageCenterX = this.GetScaledImageCenterX();
            var scaledImageCenterY = this.GetScaledImageCenterY();

            var deltaX = this.mFocusX - scaledImageCenterX;
            var deltaY = this.mFocusY - scaledImageCenterY;

            this.mMatrix.Reset();
            this.mMatrix.PostScale((float)this.ScaleFactor, (float)this.ScaleFactor);
            this.mMatrix.PostTranslate((float)deltaX, (float)deltaY);

            var curr = new PointF(e.GetX(), e.GetY());
            if (e.Action == MotionEventActions.Down)
            {
                this.start.Set(curr); // track movement
                // not best practice but is faster than batching Historical data (http://developer.android.com/reference/android/view/MotionEvent.html)
            }

            if (e.Action == MotionEventActions.Up)
            {
                var xDiff = (int)Math.Abs(curr.X - this.start.X);
                var yDiff = (int)Math.Abs(curr.Y - this.start.Y);

                // check if distance traveled in click threshold
                if (xDiff < ClickThreshold && yDiff < ClickThreshold)
                {
                    this.PerformClick();

                    var inverse = new Matrix();
                    this.mMatrix.Invert(inverse);

                    var points = new[] { e.GetX(), e.GetY() };
                    inverse.MapPoints(points);

                    this.ClickLoction?.Invoke(this, new LocationEventArgs { Point = new MapViewModel.Point(points[0], points[1]) });
                }
            }

            this.ImageMatrix = this.mMatrix;

            return true; // indicate event was handled
        }

        protected override void OnMeasure(int widthMeasureSpec, int heightMeasureSpec)
        {
            base.OnMeasure(widthMeasureSpec, heightMeasureSpec);

            // can be called several times, logic only needs to be done once though
            if (this.mViewWidth == MeasureSpec.GetSize(widthMeasureSpec) && this.mViewHeight == MeasureSpec.GetSize(heightMeasureSpec))
            {
                return;
            }

            this.mViewWidth = MeasureSpec.GetSize(widthMeasureSpec);
            this.mViewHeight = MeasureSpec.GetSize(heightMeasureSpec);

            var drawable = this.Drawable; // if no drawable
            if (drawable == null || drawable.IntrinsicWidth == 0 || drawable.IntrinsicHeight == 0)
            {
                return;
            }

            this.mImageHeight = drawable.IntrinsicHeight;
            this.mImageWidth = drawable.IntrinsicWidth;

            this.InitialiseImage();
        }

        private double GetScaledImageCenterX() => (this.mImageWidth * this.ScaleFactor) / 2;

        private double GetScaledImageCenterY() => (this.mImageHeight * this.ScaleFactor) / 2;

        private void Init(Context context)
        {
            this.ScaleFactor = 1.0f;

            this.ImageMatrix = this.mMatrix;

            this.SetScaleType(ScaleType.Matrix);

            // Setup Gesture Detectors
            this.mScaleDetector = new ScaleGestureDetector(context, new ScaleListener(this));
            this.mMoveDetector = new MoveGestureDetector(context, new MoveListener(this));
        }

        private class ScaleListener : ScaleGestureDetector.SimpleOnScaleGestureListener
        {
            private readonly TouchImageView imageView;

            public ScaleListener(TouchImageView imageView)
            {
                this.imageView = imageView;
            }

            public override bool OnScale(ScaleGestureDetector detector)
            {
                this.imageView.ScaleFactor *= detector.ScaleFactor; // scale change since previous event

                // Don't let the object get too small or too large.
                this.imageView.ScaleFactor = Math.Max(MinScale, Math.Min(this.imageView.ScaleFactor, MaxScale));
                return true;
            }
        }

        private class MoveListener : MoveGestureDetector.SimpleMoveGestureListener
        {
            private readonly TouchImageView imageView;

            public MoveListener(TouchImageView imageView)
            {
                this.imageView = imageView;
            }

            public override bool OnMove(MoveGestureDetector detector)
            {
                var d = detector.GetFocusDelta();
                this.imageView.mFocusX += d.X;
                this.imageView.mFocusY += d.Y;
                return true;
            }
        }
    }
}
