// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.Droid.Widgets.Gestures
{
    using Android.Content;
    using Android.Graphics;
    using Android.Views;

    /*
     * @author Almer Thie (code.almeros.com)
     */
#pragma warning disable SA1614 // Element parameter documentation must have text
#pragma warning disable SA1616 // Element return value documentation must have text
    public class MoveGestureDetector : BaseGestureDetector
    {
        private static readonly PointF FocusDeltaZero = new PointF();

        private readonly IMoveGestureListener mListener;
        private readonly PointF mFocusExternal = new PointF();

        private PointF mFocusDeltaExternal = new PointF();

        public MoveGestureDetector(Context context, IMoveGestureListener listener)
            : base(context)
        {
            this.mListener = listener;
        }

        /// <summary>
        /// Listener which must be implemented which is used by MoveGestureDetector
        /// to perform callbacks to any implementing class which is registered to a
        /// MoveGestureDetector via the constructor.
        /// </summary>
        /// <see cref="MoveGestureDetector.SimpleMoveGestureListener"/>
        public interface IMoveGestureListener
        {
            bool OnMove(MoveGestureDetector detector);

            bool OnMoveBegin(MoveGestureDetector detector);

            void OnMoveEnd(MoveGestureDetector detector);
        }

        public float GetFocusX() => this.mFocusExternal.X;

        public float GetFocusY() => this.mFocusExternal.Y;

        public PointF GetFocusDelta() => this.mFocusDeltaExternal;

        protected override void HandleStartProgressEvent(int actionCode, MotionEvent ev)
        {
            switch (actionCode)
            {
                case (int)MotionEventActions.Down:
                    this.ResetState(); // In case we missed an UP/CANCEL event

                    this.PrevEvent = MotionEvent.Obtain(ev);
                    this.TimeDelta = 0;

                    this.UpdateStateByEvent(ev);
                    break;

                case (int)MotionEventActions.Move:
                    this.GestureInProgress = this.mListener.OnMoveBegin(this);
                    break;
            }
        }

        protected override void HandleInProgressEvent(int actionCode, MotionEvent ev)
        {
            switch (actionCode)
            {
                case (int)MotionEventActions.Up:
                case (int)MotionEventActions.Cancel:
                    this.mListener.OnMoveEnd(this);
                    this.ResetState();
                    break;

                case (int)MotionEventActions.Move:
                    this.UpdateStateByEvent(ev);

                    // Only accept the event if our relative pressure is within
                    // a certain limit. This can help filter shaky data as a
                    // finger is lifted.
                    if (this.CurrPressure / this.PrevPressure > PressureThreshold)
                    {
                        var updatePrevious = this.mListener.OnMove(this);
                        if (updatePrevious)
                        {
                            this.PrevEvent.Recycle();
                            this.PrevEvent = MotionEvent.Obtain(ev);
                        }
                    }

                    break;
            }
        }

        protected override void UpdateStateByEvent(MotionEvent curr)
        {
            base.UpdateStateByEvent(curr);

            var prev = this.PrevEvent;

            // Focus intenal
            var mCurrFocusInternal = this.DetermineFocalPoint(curr);
            var mPrevFocusInternal = this.DetermineFocalPoint(prev);

            // Focus external
            // - Prevent skipping of focus delta when a finger is added or removed
            var mSkipNextMoveEvent = prev.PointerCount != curr.PointerCount;
            this.mFocusDeltaExternal = mSkipNextMoveEvent ? FocusDeltaZero : new PointF(mCurrFocusInternal.X - mPrevFocusInternal.X, mCurrFocusInternal.Y - mPrevFocusInternal.Y);

            // - Don't directly use mFocusInternal (or skipping will occur). Add
            //   unskipped delta values to mFocusExternal instead.
            this.mFocusExternal.X += this.mFocusDeltaExternal.X;
            this.mFocusExternal.Y += this.mFocusDeltaExternal.Y;
        }

        /// <summary>
        /// Determine (multi)finger focal point (a.k.a. center point between all fingers)
        /// </summary>
        /// <param name="e">MotionEvent</param>
        /// <returns>focal point</returns>
        private PointF DetermineFocalPoint(MotionEvent e)
        {
            // Number of fingers on screen
            var pCount = e.PointerCount;
            var x = 0f;
            var y = 0f;

            for (var i = 0; i < pCount; i++)
            {
                x += e.GetX(i);
                y += e.GetY(i);
            }

            return new PointF(x / pCount, y / pCount);
        }

        /// <summary>
        /// Helper class which may be extended and where the methods may be
        /// implemented. This way it is not necessary to implement all methods
        /// of OnMoveGestureListener.
        /// </summary>
        public class SimpleMoveGestureListener : IMoveGestureListener
        {
            public virtual bool OnMove(MoveGestureDetector detector) => false;

            public virtual bool OnMoveBegin(MoveGestureDetector detector) => true;

            public virtual void OnMoveEnd(MoveGestureDetector detector)
            {
                // Do nothing, overridden implementation may be used
            }
        }
    }
#pragma warning restore SA1614 // Element parameter documentation must have text
#pragma warning restore SA1616 // Element return value documentation must have text
}
