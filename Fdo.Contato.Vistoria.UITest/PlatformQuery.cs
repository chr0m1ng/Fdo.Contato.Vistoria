﻿using System;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace Fdo.Contato.Vistoria.UITest
{
    public class PlatformQuery
    {
        public Func<AppQuery, AppQuery> Android
        {
            set
            {
                if (AppManager.Platform == Platform.Android)
                {
                    _current = value;
                }
            }
        }

        public Func<AppQuery, AppQuery> IOS
        {
            set
            {
                if (AppManager.Platform == Platform.iOS)
                {
                    _current = value;
                }
            }
        }

        private Func<AppQuery, AppQuery> _current;
        public Func<AppQuery, AppQuery> Current
        {
            get
            {
                if (_current == null)
                {
                    throw new NullReferenceException("Trait not set for current platform");
                }
                return _current;
            }
        }
    }
}
