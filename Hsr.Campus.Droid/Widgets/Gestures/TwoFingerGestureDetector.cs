// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.Droid.Widgets.Gestures
{
    using System;
    using Android.Content;
    using Android.Views;

#pragma warning disable SA1614 // Element parameter documentation must have text
#pragma warning disable SA1616 // Element return value documentation must have text
    /*
     * @author Almer Thie (code.almeros.com)
     */
    public abstract class TwoFingerGestureDetector : BaseGestureDetector
    {
        private readonly float mEdgeSlop;

        private double mCurrLen;
        private double mPrevLen;

        protected TwoFingerGestureDetector(Context context)
            : base(context)
        {
            var config = ViewConfiguration.Get(context);
            this.mEdgeSlop = config.ScaledEdgeSlop;
        }

        protected double PrevFingerDiffX { get; set; }

        protected double PrevFingerDiffY { get; set; }

        protected double CurrFingerDiffX { get; set; }

        protected double CurrFingerDiffY { get; set; }

        /// <summary>
        /// Return the current distance between the two pointers forming the
        /// gesture in progress.
        /// </summary>
        /// <returns>Distance between pointers in pixels.</returns>
        public double GetCurrentSpan()
        {
            if (double.IsNaN(this.mCurrLen))
            {
                var cvx = this.CurrFingerDiffX;
                var cvy = this.CurrFingerDiffY;
                this.mCurrLen = Math.Sqrt((cvx * cvx) + (cvy * cvy));
            }

            return this.mCurrLen;
        }

        /// <summary>
        /// Return the previous distance between the two pointers forming the
        /// gesture in progress.
        /// </summary>
        /// <returns>Previous distance between pointers in pixels.</returns>
        public double GetPreviousSpan()
        {
            if (double.IsNaN(this.mPrevLen))
            {
                var pvx = this.PrevFingerDiffX;
                var pvy = this.PrevFingerDiffY;
                this.mPrevLen = Math.Sqrt((pvx * pvx) + (pvy * pvy));
            }

            return this.mPrevLen;
        }

        /// <summary>
        /// MotionEvent has no getRawX(int) method; simulate it pending future API approval.
        /// </summary>
        /// <param name="ev"></param>
        /// <param name="pointerIndex"></param>
        /// <returns></returns>
        protected static float GetRawX(MotionEvent ev, int pointerIndex)
        {
            var offset = ev.GetX() - ev.RawX;
            if (pointerIndex < ev.PointerCount)
            {
                return ev.GetX(pointerIndex) + offset;
            }

            return 0f;
        }

        /// <summary>
        ///  MotionEvent has no getRawY(int) method; simulate it pending future API approval.
        /// </summary>
        /// <param name="ev"></param>
        /// <param name="pointerIndex"></param>
        /// <returns></returns>
        protected static float GetRawY(MotionEvent ev, int pointerIndex)
        {
            var offset = ev.GetY() - ev.RawY;
            if (pointerIndex < ev.PointerCount)
            {
                return ev.GetY(pointerIndex) + offset;
            }

            return 0f;
        }

        protected override void UpdateStateByEvent(MotionEvent curr)
        {
            base.UpdateStateByEvent(curr);

            var prev = this.PrevEvent;

            this.mCurrLen = double.NaN;
            this.mPrevLen = double.NaN;

            // Previous
            var px0 = prev.GetX(0);
            var py0 = prev.GetY(0);
            var px1 = prev.GetX(1);
            var py1 = prev.GetY(1);
            var pvx = px1 - px0;
            var pvy = py1 - py0;
            this.PrevFingerDiffX = pvx;
            this.PrevFingerDiffY = pvy;

            // Current
            var cx0 = curr.GetX(0);
            var cy0 = curr.GetY(0);
            var cx1 = curr.GetX(1);
            var cy1 = curr.GetY(1);
            var cvx = cx1 - cx0;
            var cvy = cy1 - cy0;
            this.CurrFingerDiffX = cvx;
            this.CurrFingerDiffY = cvy;
        }

        /// <summary>
        /// Check if we have a sloppy gesture. Sloppy gestures can happen if the edge
        /// of the user's hand is touching the screen, for example.
        /// </summary>
        /// <param name="ev"></param>
        /// <returns></returns>
        protected bool IsSloppyGesture(MotionEvent ev)
        {
            // As orientation can change, query the metrics in touch down
            var metrics = this.Context.Resources.DisplayMetrics;

            var edgeSlop = this.mEdgeSlop;
            var rightSlop = metrics.WidthPixels - this.mEdgeSlop;
            var bottomSlop = metrics.HeightPixels - this.mEdgeSlop;

            var x0 = ev.RawX;
            var y0 = ev.RawY;
            var x1 = GetRawX(ev, 1);
            var y1 = GetRawY(ev, 1);

            var p0Sloppy = x0 < edgeSlop || y0 < edgeSlop
                    || x0 > rightSlop || y0 > bottomSlop;
            var p1Sloppy = x1 < edgeSlop || y1 < edgeSlop
                    || x1 > rightSlop || y1 > bottomSlop;

            return p0Sloppy || p1Sloppy;
        }
    }
#pragma warning restore SA1614 // Element parameter documentation must have text
#pragma warning restore SA1616 // Element return value documentation must have text
}
