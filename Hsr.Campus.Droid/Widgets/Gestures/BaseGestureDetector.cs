// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.Droid.Widgets.Gestures
{
    using Android.Content;
    using Android.Views;

#pragma warning disable SA1614 // Element parameter documentation must have text
#pragma warning disable SA1616 // Element return value documentation must have text

    public abstract class BaseGestureDetector
    {
        /// <summary>
        /// This value is the threshold ratio between the previous combined pressure
        /// and the current combined pressure. When pressure decreases rapidly
        /// between events the position values can often be imprecise, as it usually
        /// indicates that the user is in the process of lifting a pointer off of the
        /// device. This value was tuned experimentally.
        /// </summary>
        protected const float PressureThreshold = 0.67f;
        private MotionEvent mCurrEvent;

        protected BaseGestureDetector(Context context)
        {
            this.Context = context;
        }

        protected Context Context { get; }

        protected bool GestureInProgress { get; set; }

        protected MotionEvent PrevEvent { get; set; }

        protected float CurrPressure { get; set; }

        protected float PrevPressure { get; set; }

        protected long TimeDelta { get; set; }

        /// <summary>
        /// All gesture detectors need to be called through this method to be able to
        /// detect gestures. This method delegates work to handler methods
        /// (handleStartProgressEvent, handleInProgressEvent) implemented in
        /// extending classes.
        /// </summary>
        /// <param name="ev"></param>
        /// <returns></returns>
        public bool OnTouchEvent(MotionEvent ev)
        {
            var actionCode = (int)ev.Action & (int)MotionEventActions.Mask;
            if (!this.GestureInProgress)
            {
                this.HandleStartProgressEvent(actionCode, ev);
            }
            else
            {
                this.HandleInProgressEvent(actionCode, ev);
            }

            return true;
        }

        /// <summary>
        /// Returns {@code true} if a gesture is currently in progress.
        /// </summary>
        /// <returns>{@code true} if a gesture is currently in progress, {@code false} otherwise.</returns>
        public bool IsInProgress() => this.GestureInProgress;

        /// <summary>
        /// Return the time difference in milliseconds between the previous accepted
        /// GestureDetector event and the current GestureDetector event.
        /// </summary>
        /// <returns>Time difference since the last move event in milliseconds.</returns>
        public long GetTimeDelta() => this.TimeDelta;

        /// <summary>
        /// Return the event time of the current GestureDetector event being
        /// processed.
        /// </summary>
        /// <returns>Current GestureDetector event time in milliseconds.</returns>
        public long GetEventTime() => this.mCurrEvent.EventTime;

        /// <summary>
        /// Called when the current event occurred when NO gesture is in progress
        /// yet. The handling in this implementation may set the gesture in progress
        /// (via mGestureInProgress) or out of progress
        /// </summary>
        /// <param name="actionCode"></param>
        /// <param name="ev"></param>
        protected abstract void HandleStartProgressEvent(int actionCode, MotionEvent ev);

        /// <summary>
        /// Called when the current event occurred when a gesture IS in progress. The
        /// handling in this implementation may set the gesture out of progress (via
        /// mGestureInProgress).
        /// </summary>
        /// <param name="actionCode"></param>
        /// <param name="ev"></param>
        protected abstract void HandleInProgressEvent(int actionCode, MotionEvent ev);

        protected virtual void UpdateStateByEvent(MotionEvent curr)
        {
            var prev = this.PrevEvent;

            // Reset mCurrEvent
            if (this.mCurrEvent != null)
            {
                this.mCurrEvent.Recycle();
                this.mCurrEvent = null;
            }

            this.mCurrEvent = MotionEvent.Obtain(curr);

            // Delta time
            this.TimeDelta = curr.EventTime - prev.EventTime;

            // Pressure
            this.CurrPressure = curr.GetPressure(curr.ActionIndex);
            this.PrevPressure = prev.GetPressure(prev.ActionIndex);
        }

        protected void ResetState()
        {
            if (this.PrevEvent != null)
            {
                this.PrevEvent.Recycle();
                this.PrevEvent = null;
            }

            if (this.mCurrEvent != null)
            {
                this.mCurrEvent.Recycle();
                this.mCurrEvent = null;
            }

            this.GestureInProgress = false;
        }
    }
#pragma warning restore SA1614 // Element parameter documentation must have text
#pragma warning restore SA1616 // Element return value documentation must have text
}
