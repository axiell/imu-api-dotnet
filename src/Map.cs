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
using System;
using System.Collections.Generic;

namespace IMu
{
    /*!
    ** Provides a simple map class with `String` keys and a set of convenience 
	** methods for getting values of certain types.
    ** 
	** @extends System.Collections.Generic.Dictionary<string, object>
	**
    ** @usage
    **   IMu.Map
    ** @end
    ** 
    ** @since 1.0
    */
    /// <summary>
    /// Provides a simple map class with `String` keys and a set of convenience 
    /// methods for getting values of certain types.
    /// </summary>
    public class Map : Dictionary<string,object>
    {
		/* Properties */
        /*!
        ** Gets the value associated with the key ``name`` and returns it as a 
		** `Boolean`.
        ** 
        ** @param name
        **   The key whose associated value is to be returned.
        **   
        ** @returns
        **   The value, interpreted as a `Boolean`.
		**   ``Null`` values are considered ``false``.
        **   Numeric values are considered ``false`` if they evaluate to 
		**   ``0`` and ``true`` otherwise. 
		**   Any other non-boolean value is converted to a `String` and then 
		**   parsed as a `Boolean`.
        */
        /// <summary>
        /// The key whose associated value is to be returned.
        /// </summary>
        /// <param name="name">The key whose associated value is to be returned.</param>
        /// <returns>The value, interpreted as a Boolean. Null values are considered false.
        /// Numeric values are considered false if they evaluate to 0 and true 
        /// otherwise. Any other non-boolean value is converted to a string and 
        /// then parsed as a Boolean.</returns>
        public bool
        GetBool(string name)
        {
            object value = this[name];
            if (value == null)
                return false;
            if (value is bool)
                return (bool) value;

            if (value is int)
                return Convert.ToBoolean((int) value);
            if (value is long)
                return Convert.ToBoolean((long) value);
            if (value is double)
                return Convert.ToBoolean((double) value);

            return bool.Parse(value.ToString());
        }

        /*!
        ** A convenince for VB users.
		**
		** Gets the value associated with the key ``name`` and returns it as a 
        ** `Boolean`.
        ** 
        ** @param name
        **   The key whose associated value is to be returned.
        **   
        ** @returns
        **   The value, interpreted as a Boolean. 
        **   ``Null`` values are considered ``false``.
        **   Numeric values are considered ``false`` if they evaluate to 
		**   ``0`` and ``true`` otherwise. 
        **   Any other non-boolean value is converted to a `String` and then 
        **   parsed as a `Boolean`.
        */
        /// <summary>
        /// The key whose associated value is to be returned.
        /// </summary>
        /// <param name="name">The key whose associated value is to be returned.</param>
        /// <returns>The value, interpreted as a Boolean. Null values are considered false.
        /// Numeric values are considered false if they evaluate to 0 and true 
        /// otherwise. Any other non-boolean value is converted to a string and 
        /// then parsed as a Boolean.</returns>
        public bool
        GetBoolean(string name)
        {
            // Convenience for VB users
            return GetBool(name);
        }

        /*!
        ** Gets the value associated with the key ``name`` and returns it as a 
		** double precision floating point number.
        ** 
        ** @param name
        **   The key whose associated value is to be returned.
        **   
        ** @returns
        **   The value, interpreted as a `double`.
        **   ``Null`` values evalueate to ``0``.
		**   `Boolean` values evaluate to 0 if ``false`` and 1 if ``true``.
        **   Any other non-numeric value is converted to a `String` and then
        **   parsed as a `double`.
        */
        /// <summary>
        /// Gets the value associated with the key name and returns it as a double.
        /// </summary>
        /// <param name="name">The key whose associated value is to be returned.</param>
        /// <returns>The value, interpreted as a double precision floating point
        /// number. Null values evaluate to 0. Boolean values evaluate to 0 if false
        /// and 1 if true. Any other non-numeric value is converted to a string
        /// and then parsed as a double.</returns>
        public double
        GetDouble(string name)
        {
            object value = this[name];
            if (value == null)
                return 0;
            if (value is double)
                return (double) value;

            if (value is bool)
                return Convert.ToDouble((bool) value);
            if (value is int)
                return Convert.ToDouble((int) value);
            if (value is long)
                return Convert.ToDouble((long )value);

            return double.Parse(value.ToString());
        }

        /*!
        ** Gets the value associated with the key ``name`` and returns it as an 
		** `int`.
        ** 
        ** @param name
        **   The key whose associated value is to be returned.
        **   
        ** @returns 
        **   The value, interpreted as an `int`. 
		**   ``Null`` values evaluate to ``0``.
        **   `Boolean` values evaluate to ``0`` if ``false`` and ``1`` if 
		**   ``true``. 
		**   Any other non-numeric value is converted to a `String` and then 
		**   parsed as an `int`.
        */
        /// <summary>
        /// Gets the value associated with the key name and returns it as an integer.
        /// </summary>
        /// <param name="name">The key whose associated value is to be returned.</param>
        /// <returns>The value, interpreted as an integer. Null values evaluate to 0.
        /// Boolean values evaluate to 0 if false and 1 if true. Any other 
        /// non-numeric value is converted to a string and then parsed as 
        /// an integer.</returns>
        public int
        GetInt(string name)
        {
            object value = this[name];
            if (value == null)
                return 0;
            if (value is int)
                return (int) value;

            if (value is bool)
                return Convert.ToInt32((bool) value);
            if (value is long)
                return Convert.ToInt32((long) value);
            if (value is double)
                return Convert.ToInt32((double) value);

            return int.Parse(value.ToString());
        }

        /*!
		** A convenience for VB users.
		**
        ** Gets the value associated with the key ``name`` and returns it as an 
		** `int`.
        ** 
        ** @param name
        **   The key whose associated value is to be returned.
        **   
        ** @returns 
        **   The value, interpreted as an `int`.
		**   ``Null`` values evaluate to ``0``.
        **   `Boolean` values evaluate to ``0`` if ``false`` and ``1`` if 
		**   ``true``. 
		**   Any other non-numeric value is converted to a `String` and then 
		**   parsed as an `integer`.
        */
        /// <summary>
        /// Gets the value associated with the key name and returns it as an integer.
        /// </summary>
        /// <param name="name">The key whose associated value is to be returned.</param>
        /// <returns>The value, interpreted as an integer. Null values evaluate to 0.
        /// Boolean values evaluate to 0 if false and 1 if true. Any other 
        /// non-numeric value is converted to a string and then parsed as 
        /// an integer.</returns>
        public int
        GetInteger(string name)
        {
            // Convenience for VB users
            return GetInt(name);
        }

        /*!
        ** Gets the value associated with the key ``name`` and returns it as a 
		** long integer.
        ** 
        ** @param name
        **   The key whose associated value is to be returned.
        **   
        ** @returns
        **   The value, interpreted as a `long`.
		**   ``Null`` values evaluate to ``0``.
        **   `Boolean` values evaluate to ``0`` if ``false`` and ``1`` if 
		**   ``true``. 
		**   Any other non-numeric value is converted to a `String` and then 
		**   parsed as a `long`.
        */
        /// <summary>
        /// Gets the value associated with the key name and returns it as a long integer.
        /// </summary>
        /// <param name="name">The key whose associated value is to be returned.</param>
        /// <returns>The value, interpreted as a long integer. Null values evaluate to 0.
        /// Boolean values evaluate to 0 if false and 1 if true. Any other non-numeric
        /// value is converted to a string and then parsed as a long.</returns>
        public long
        GetLong(string name)
        {
            object value = this[name];
            if (value == null)
                return 0;
            if (value is long)
                return (long) value;

            if (value is bool)
                return Convert.ToInt64((bool) value);
            if (value is int)
                return Convert.ToInt64((int) value);
            if (value is double)
                return Convert.ToInt64((double) value);

            return long.Parse(value.ToString());
        }

        /*!
        ** Gets the value associated with the key ``name`` and returns it as an 
        ** IMu `Map` object [$<link>(:map:map)].
        ** 
        ** @param name
        **   The key whose associated value is to be returned.
        **   
        ** @returns
        **   The value, cast to a `Map` [$<link>(:map:map)].
        */
        /// <summary>
        /// Gets the value associated with the key name and returns it as an 
        /// IMu Map object.
        /// </summary>
        /// <param name="name">The key whose associated value is to be returned.</param>
        /// <returns>The value, cast to a Map.</returns>
        public Map
        GetMap(string name)
        {
            return this[name] as Map;
        }

        /*!
        ** Gets the value associated with the key ``name`` and returns it as an
        ** array of IMu `Map` objects [$<link>(:map:map)].
        ** 
        ** @param name
        **   The key whose associated value is to be returned.
        **   
        ** @returns
        **   The value, cast to an array of `Map` objects [$<link>(:map:map)].
        */
        /// <summary>
        /// Gets the value associated with the key name and returns it as an 
        /// array of IMu Map objects.
        /// </summary>
        /// <param name="name">The key whose associated value is to be returned.</param>
        /// <returns>The value, cast to an array of Map objects.</returns>
        public Map[]
        GetMaps(string name)
        {
            object value = this[name];
            if (value == null)
                return null;
            object[] list = value as object[];
            Map[] maps = new Map[list.Length];
            Array.Copy(list, maps, list.Length);
            return maps;
        }

        /*!
        ** Gets the value associated with the key ``name`` and returns it as a 
		** `String`.
        ** 
        ** @param name
        **   The key whose associated value is to be returned.
        **   
        ** @returns
        **   The value, interpreted as a `String`.
		**   ``Null`` values remain ``null``. 
		**   Any other non-string value is converted to a `String` using the 
		**   object's **ToString( )** method.
        */
        /// <summary>
        /// Gets the value associated with the key name and returns it as a string.
        /// </summary>
        /// <param name="name">The key whose associated value is to be returned.</param>
        /// <returns>The value, interpreted as a string. Null values remain null. 
        /// Any other non-string value is converted to a string using the object's
        /// ToString method.</returns>
        public string
        GetString(string name)
        {
            object value = this[name];
            if (value == null)
                return null;
            if (value is string)
                return value as string;

            return value.ToString();
        }

        /*!
        ** Gets the value associated with the key ``name`` and returns it as an
        ** array of `String`\s.
        ** 
        ** @param name
        **   The key whose associated value is to be returned.
        **   
        ** @returns
        **   The value, converted to an array of `string`\s.
        */
        /// <summary>
        /// Gets the value associated with the key name and returns it as an
        /// array of strings.
        /// </summary>
        /// <param name="name">The key whose associated value is to be returned.</param>
        /// <returns>The value, converted to an array of strings.</returns>
        public string[]
        GetStrings(string name)
        {
            object value = this[name];
            if (value == null)
                return null;
            object[] list = value as object[];
            string[] strings = new string[list.Length];
            Array.Copy(list, strings, list.Length);
            return strings;
        }
    }
}
