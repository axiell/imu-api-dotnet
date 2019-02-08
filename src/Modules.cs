/* KE Software Open Source Licence
** 
** Notice: Copyright (c) 2011-2013 KE SOFTWARE PTY LTD (ACN 006 213 298)
** (the "Owner"). All rights reserved.
** 
** Licence: Permission is hereby granted, free of charge, to any person
** obtaining a copy of this software and associated documentation files
** (the "Software"), to deal with the Software without restriction,
** including without limitation the rights to use, copy, modify, merge,
** publish, distribute, sublicense, and/or sell copies of the Software,
** and to permit persons to whom the Software is furnished to do so,
** subject to the following conditions.
** 
** Conditions: The Software is licensed on condition that:
** 
** (1) Redistributions of source code must retain the above Notice,
**     these Conditions and the following Limitations.
** 
** (2) Redistributions in binary form must reproduce the above Notice,
**     these Conditions and the following Limitations in the
**     documentation and/or other materials provided with the distribution.
** 
** (3) Neither the names of the Owner, nor the names of its contributors
**     may be used to endorse or promote products derived from this
**     Software without specific prior written permission.
** 
** Limitations: Any person exercising any of the permissions in the
** relevant licence will be taken to have accepted the following as
** legally binding terms severally with the Owner and any other
** copyright owners (collectively "Participants"):
** 
** TO THE EXTENT PERMITTED BY LAW, THE SOFTWARE IS PROVIDED "AS IS",
** WITHOUT ANY REPRESENTATION, WARRANTY OR CONDITION OF ANY KIND, EXPRESS
** OR IMPLIED, INCLUDING (WITHOUT LIMITATION) AS TO MERCHANTABILITY,
** FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. TO THE EXTENT
** PERMITTED BY LAW, IN NO EVENT SHALL ANY PARTICIPANT BE LIABLE FOR ANY
** CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT,
** TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE
** SOFTWARE OR THE USE OR OTHER DEALINGS WITH THE SOFTWARE.
** 
** WHERE BY LAW A LIABILITY (ON ANY BASIS) OF ANY PARTICIPANT IN RELATION
** TO THE SOFTWARE CANNOT BE EXCLUDED, THEN TO THE EXTENT PERMITTED BY
** LAW THAT LIABILITY IS LIMITED AT THE OPTION OF THE PARTICIPANT TO THE
** REPLACEMENT, REPAIR OR RESUPPLY OF THE RELEVANT GOODS OR SERVICES
** (INCLUDING BUT NOT LIMITED TO SOFTWARE) OR THE PAYMENT OF THE COST OF SAME.
*/
namespace IMu
{
	public class Modules : Handler
	{
		/* Constructors */
		public Modules(Session session)
			: base(session)
		{
			Initialise();
		}

		public Modules()
			: base()
		{
			Initialise();
		}
		
		/* Methods */
		public int
		AddFetchSet(string name, Map set)
		{
			Map args = new Map();
			args.Add("name", name);
			args.Add("set", set);
			long count = (long) Call("addFetchSet", args);
            return (int) count;
		}
		
		public int
		AddFetchSets(Map sets)
		{
			long count = (long) Call("addFetchSets", sets);
            return (int) count;
		}
		
		public int
		AddSearchAlias(string name, Map set)
		{
			Map args = new Map();
			args.Add("name", name);
			args.Add("set", set);
            long count = (long) Call("addSearchAlias", args);
            return (int) count;
		}
		
		public int
		AddSearchAliases(Map aliases)
		{
			long count = (long) Call("addSearchAliases", aliases);
            return (int) count;
		}
		
		public int
		AddSortSet(string name, Map set)
		{
			Map args = new Map();
			args.Add("name", name);
            args.Add("set", set);
			long count = (long) Call("addSortSet", args);
            return (int) count;
		}
		
		public int
		AddSortSets(Map sets)
		{
			long count = (long) Call("addSortSets", sets);
            return (int) count;
		}

		public ModulesFetchResult
		Fetch(string flag, long offset, int count, string columns)
		{
			return DoFetch(flag, offset, count, columns);
		}

		public ModulesFetchResult
		Fetch(string flag, long offset, int count)
		{
			return DoFetch(flag, offset, count, null);
		}

		public ModulesFetchResult
		Fetch(ModulesFetchPosition pos, int count, string columns)
		{
			return DoFetch(pos._flag, pos._offset, count, columns);
		}

		public ModulesFetchResult
		Fetch(ModulesFetchPosition pos, int count)
		{
			return DoFetch(pos._flag, pos._offset, count, null);
		}

		public long
		FindAttachments(string table, string column, long key)
		{
			Map args = new Map();
            args.Add("table", table);
            args.Add("column", column);
            args.Add("key", key);
			return (long) Call("findAttachments", args);
		}

        public string[]
        FindKeys(ModulesKey[] keys, string[] include)
        {
            return DoFindKeys(keys, include);
        }

        public string[]
        FindKeys(ModulesKey[] keys, string include)
        {
            return DoFindKeys(keys, include);
        }

        public string[]
        FindKeys(ModulesKey[] keys)
        {
            return DoFindKeys(keys, null);
        }

        public string[]
        FindKeys(ModulesKeys keys, string[] include)
        {
            return DoFindKeys(keys.ToArray(), include);
        }

        public string[]
        FindKeys(ModulesKeys keys, string include)
        {
            return DoFindKeys(keys.ToArray(), include);
        }

        public string[]
        FindKeys(ModulesKeys keys)
        {
            return DoFindKeys(keys.ToArray(), null);
        }

        public string[]
		FindTerms(Terms terms, string[] include)
		{
			return DoFindTerms(terms, include);
		}

		public string[]
        FindTerms(Terms terms, string include)
		{
			return DoFindTerms(terms, include);
		}

		public string[]
        FindTerms(Terms terms)
		{
			return DoFindTerms(terms, null);
		}
		
		public long
		GetHits(string module)
		{
			return (long) Call("getHits", module);
		}
		
		public long
		GetHits()
		{
			return (long) Call("getHits");
		}

		public long
		RestoreFromFile(string file)
		{
			Map args = new Map();
			args.Add("file", file);
			return (long) Call("restoreFromFile", args);
		}

		public long
		RestoreFromFile(string file, string module)
		{
			Map args = new Map();
			args.Add("file", file);
			args.Add("module", module);
			return (long) Call("restoreFromFile", args);
		}

		public long
		RestoreFromTemp(string file)
		{
			Map args = new Map();
			args.Add("file", file);
			return (long) Call("restoreFromTemp", args);
		}

		public long
		RestoreFromTemp(string file, string module)
		{
			Map args = new Map();
			args.Add("file", file);
			args.Add("module", module);
			return (long) Call("restoreFromTemp", args);
		}
		
		public int
		SetModules(string[] modules)
		{
			return (int) Call("setModules", modules);
		}
		
		public int
		SetModules(string modules)
		{
			return (int) Call("setModules", modules);
		}
		
		public int
		setModules()
		{
			return (int) Call("setModules");
		}
		
		public object
		Sort(Map set, string[] flags)
		{
			return DoSort(set, flags);
		}

        public object
		Sort(Map set, string flags)
		{
			return DoSort(set, flags);
		}
		
		public object
		Sort(Map set)
		{
			return DoSort(set, null);
		}
		
		protected ModulesFetchResult
		DoFetch(string flag, long offset, int count, string columns)
		{
			Map args = new Map();
            args.Add("flag", flag);
            args.Add("offset", offset);
            args.Add("count", count);
			if (columns != null)
                args.Add("columns", columns);
			Map data = (Map) Call("fetch", args);
			
			ModulesFetchResult result = new ModulesFetchResult();
			result._count = data.GetInt("count");
			
			object[] list = (object[]) data["modules"];
			result._modules = new ModulesFetchModule[list.Length];
			for (int i = 0; i < list.Length; i++)
			{
				Map map = (Map) list[i];
				ModulesFetchModule module = new ModulesFetchModule();
				module._hits = map.GetLong("hits");
				module._index = map.GetInt("index");
				module._name = map.GetString("name");
                object[] rows = map["rows"] as object[];
                module._rows = new Map[rows.Length];
                for (int j = 0; j < rows.Length; j++)
                    module._rows[j] = rows[j] as Map;

				result._modules[i] = module;
			}
			
			if (data.ContainsKey("current"))
				result._current = MakePosition(data["current"]);
			if (data.ContainsKey("prev"))
				result._prev = MakePosition(data["prev"]);
			if (data.ContainsKey("next"))
				result._next = MakePosition(data["next"]);
			
			return result;
		}
		
		protected string[]
		DoFindKeys(ModulesKey[] keys, object include)
		{
			Map args = new Map();

            object[] list = new object[keys.Length];
            for (int i = 0; i < keys.Length; i++)
            {
                object[] item = { keys[i].Module, keys[i].Key };
                list[i] = item;
            }
            args.Add("keys", list);

			if (include != null)
                args.Add("include", include);

			object[] data = (object[]) Call("findKeys", args);
			string[] result = new string[data.Length];
			for (int i = 0; i < data.Length; i++)
				result[i] = data[i] as string;
			return result;
		}
		
		protected string[]
		DoFindTerms(Terms terms, object include)
		{
			Map args = new Map();
            args.Add("terms", terms.ToArray());
			if (include != null)
                args.Add("include", include);
			object[] data = (object[]) Call("findTerms", args);
			string[] result = new string[data.Length];
			for (int i = 0; i < data.Length; i++)
				result[i] = data[i] as string;
			return result;
		}
		
		protected object
		DoSort(Map set, object flags)
		{
			Map args = new Map();
			args.Add("set", set);
			if (flags != null)
				args.Add("flags", flags);
			return Call("sort", args);
		}

		protected void
		Initialise()
		{
			_name = "Modules";
		}
		
		protected ModulesFetchPosition
		MakePosition(object raw)
		{	
			Map map = (Map) raw;
			string flag = map.GetString("flag");
			long offset = map.GetLong("offset");
			return new ModulesFetchPosition(flag, offset);
		}
	}
}
