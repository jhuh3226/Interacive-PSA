
using UnityEngine;
using System.Collections.Generic;
using System;
using System.Threading;

namespace SkinnedDecals {

	public class ThreadPooler : MonoBehaviour {

		private static bool initialized = false;

		public static int totalThreads = 8;
		private static int numThreads = 0;

		private List<Action> actions = new List<Action>();
		private List<Action> currentActions = new List<Action>();

		#region Instance

		private static ThreadPooler instance;
		public static ThreadPooler Instance {
			get {
				Initialize();
				return instance;
			}
		}

		void OnDestroy() {
			if(instance == this) {
				initialized = false;
				instance = null;
			}
		}

		#endregion

		#region Initialization

		void Awake() {
			instance = this;
			initialized = true;
		}

		static void Initialize() {
			if(!initialized) {
				if(!Application.isPlaying)
					return;

				initialized = true;

				GameObject go = new GameObject("ThreadPooler");
				instance = go.AddComponent<ThreadPooler>();
			}

		}

		#endregion

		#region Runtime

		public static void RunOnMainThread(Action action) {
			lock(Instance.actions) {
				Instance.actions.Add(action);
			}
		}

		public static Thread RunOnThread(Action action) {
			Initialize();

			while(numThreads >= totalThreads) {
				Thread.Sleep(1);
			}

			Interlocked.Increment(ref numThreads);
			ThreadPool.QueueUserWorkItem(RunAction, action);

			return null;
		}

		private static void RunAction(object action) {
			try {
				((Action)action)();
			} catch(Exception e) {
				Debug.LogError("Exception while trying to run action: " + e.Message + '\n' + e.StackTrace);
			} finally {
				Interlocked.Decrement(ref numThreads);
			}

		}

		void Update() {
			lock(actions) {
				currentActions.Clear();
				currentActions.AddRange(actions);
				actions.Clear();
			}
			for(int i = 0; i < currentActions.Count; i++)
				currentActions[i]();
		}

		#endregion
	}
}