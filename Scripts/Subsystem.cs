

        /// <summary>
        /// Structure for initialization of a subsystem.
        /// </summary>
        public struct Subsystem
        {
            /// <summary> Subsystem name </summary>
            public string name;        
        
            /// <summary> Called for each sub-system to initialize </summary>
            public Action preInitialize;
        
            /// <summary> Called after all sub-system InitA was called </summary>
            public Action initialize;
        
            /// <summary> Called for each entry </summary>
            public Action<object> initializeObject;
        
            /// <summary> Called for each sub-system- to de-initialize </summary>
            public Action deInitialize;
        
            /// <summary> Object type for this Subsystem </summary>
            public Type entryType;
        }
