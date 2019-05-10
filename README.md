# IMu API for .Net #

This documentation describes using the IMu API for .Net. It is applicable to both C# and Visual Basic programmers.

## Data Types

C# and Visual Basic .Net programmers are familiar with two kinds of data types. Each language has both its own conventional types and standardised .Net types. For example, an integer variable is declared idiomatically in C# as `int` while in Visual Basic it is declared as `Integer`. The standard .Net type for both of these is `Int32`.

This documentation uses conventional types wherever possible. Using conventional types in documentation makes it easier for the reader to follow the code examples. However, this creates a minor problem in the text where the type of a variable or property or the return value of a method is referred to. For example, the `AddFetchSet` method of IMu’s Module class returns an `int` in C# and an `Integer` in VB (both of which are equivalent to the standard .Net type `Int32`). Explicitly describing the method’s return value for both C# and VB this way is cumbersome and confusing. To avoid this, where a data type is referred to in the general text, a generic term for the type is usually used instead. For example, the `AddFetchSet` method is described as returning an integer. Similarly a method such as `FindKey` which returns a `long` in C# and a `Long` in VB (both of which are equivalent to the standard .Net type `Int64`) is described as returning a long integer.

# Contents

* [Using The IMu API](#1-using-the-imu-api)
    * [Test Program](#1-1-test-program)
    * [Exceptions](#1-2-exceptions)
* [Connecting to an IMu server](#2-connecting-to-an-imu-server)
    * [Handlers](#2-1-handlers)
* [Accessing an EMu Module](#3-accessing-an-emu-module)
    * [Searching a Module](#3-1-searching-a-module)
        * [The findKey Method](#3-1-1-the-findkey-method)
        * [The findKeys Method](#3-1-2-the-findkeys-method)
        * [The findTerms Method](#3-1-3-the-findterms-method)
        * [The findWhere Method](#3-1-4-the-findwhere-method)
        * [Number of Matches](#3-1-5-number-of-matches)
    * [Sorting](#3-2-sorting)
        * [The sort Method](#3-2-1-the-sort-method)
    * [Getting Information from Matching Records](#3-3-getting-information-from-matching-records)
        * [The fetch Method](#3-3-1-the-fetch-method)
        * [Specifying Columns](#3-3-2-specifying-columns)
        * [Example](#3-3-3-example)
    * [Multimedia](#3-4-multimedia)
        * [Multimedia Attachments](#3-4-1-multimedia-attachments)
        * [Multimedia Files](#3-4-2-multimedia-files)
        * [Filters](#3-4-3-filters)
        * [Modifiers](#3-4-4-modifiers)
* [Maintaining State](#4-maintaining-state)
    * [Example](#4-1-example)
* [Logging in to an IMu server](#5-logging-in-to-an-imu-server)
    * [The login method](#5-1-the-login-method)
    * [The logout method](#5-2-the-logout-method)
* [Updating an EMu Module](#6-updating-an-emu-module)
    * [The insert Method](#6-1-the-insert-method)
    * [The update Method](#6-2-the-update-method)
    * [The remove Method](#6-3-the-remove-method)
* [Exceptions](#7-exceptions)

<h1 id="1-using-the-imu-api">Using The IMu API</h1>

The IMu .Net API source code bundle for version 2.0 (or higher) is required to develop an IMu-based application. This bundle contains all the classes that make up the IMu .Net API. IMu API bundles are available from the IMu [releases](https://github.com/axiell/imu-api-dotnet/releases) page.

As with all .Net assemblies, the IMu .Net assembly must be available so that the .Net compiler and runtime environment can find and use the IMu classes. Tools for .Net development, such as Microsoft’s Visual Studio, make it possible to add a reference to the IMu assembly to a project. All classes in the IMu .Net API are included in the one namespace, IMu. As is usual in .Net development, it is possible to refer to an IMu class in your code by either:

1. 
    Using the fully qualified name:

    C#
    ```
    IMu.Session session = new IMu.Session();
    ```

    VB
    ```
    Dim session = New IMu.Session()
    ```

1. 
    Importing the namespace:

    C#
    ```
    using IMu;
    // ⋯
    Session session = new Session();
    ```

    VB
    ```
    Imports IMu
    ' ⋯
    Dim session = New Session()
    ```

<h2 id="1-1-test-program">Test Program</h2>

Compiling and running this very simple console-based IMu program is a good test of whether the development environment has been set up properly for using IMu:

C#
```
using System;
using IMu;

class Hello
{
    static void Main(string[] args)
    {
        Console.WriteLine("IMu Version {0}", IMu.IMu.VERSION);
        Environment.Exit(0);
    }
}
```

VB
```
Imports IMu

Module Hello
    Sub Main()
        Console.WriteLine("IMu Version {0}", IMu.IMu.VERSION)
        Environment.Exit(0)
    End Sub
End Module
```

The IMu library includes a class called `IMu`. This class includes the static `string` member `VERSION` which contains the version of this IMu release.


<h2 id="1-2-exceptions">Exceptions</h2>

Many of the methods in the IMu library objects throw an exception when an error occurs. For this reason, code that uses IMu library objects should be surrounded with an `try/catch` block.

The following code is a basic template for writing .Net programs that use the IMu library:

C#
```
using IMu;
// ⋯
try
{
    // Create and use IMu objects
    // ⋯
}
catch (Exception e)
{
    // Handle or report error
    // ⋯
}
```

VB
```
Imports IMu
' ⋯
Try
    ' Create and use IMu objects
    ' ⋯
Catch ex As Exception
    ' Handle or report error
    ' ⋯
End Try
```

Most IMu exceptions throw an `IMuException` object. The `IMuException` class is a subclass of the standard .Net `Exception`. In many cases your code can simply catch the standard `Exception` (as in this template). If more information is required about the exact `IMuException` thrown, see [Exceptions](#7-exceptions).

> **NOTE:**
>
> Many of the examples that follow assume that code fragments have been surrounded with code structured in this way.

<h1 id="2-connecting-to-an-imu-server">Connecting to an IMu Server</h1>

Most IMu based programs begin by creating a connection to an IMu server. Connections to a server are created and managed using IMu’s `Session` class. Before connecting, both the name of the host and the port number to connect on must be specified. This can be done in one of three ways.

1. 
    The simplest way to create a connection to an IMu server is to pass the host name and port number to the `Session` constructor and then call the `Connect` method. For example:

    C#
    ```
    using IMu;
    // ⋯
    Session session = new Session("server.com", 12345);
    session.Connect();
    ```

    VB
    ```
    Imports IMu
    ' ⋯
    Dim session = New Session("server.com", 12345)
    session.Connect()
    ```

1. 
    Alternatively, pass no values to the constructor and then set the `Host` and `Port` properties before calling `Connect`:

    C#
    ```
    using IMu;
    // ⋯
    Session session = new Session();
    session.Host = "server.com";
    session.Port = 12345;
    session.Connect();
    ```

    VB
    ```
    Imports IMu
    ' ⋯
    Dim session as Session = New Session
    session.Host = "server.com"
    session.Port = 12345
    session.Connect()
    ```

1. 
    If either the host or port is not set, the `Session` class default value will be used. These defaults can be overridden by setting the (static) class properties `DefaultHost` and `DefaultPort`:

    C#
    ```
    using IMu;
    // ⋯
    Session.DefaultHost = "server.com";
    Session.DefaultPort = 12345;
    Session session = new Session();
    session.Connect();
    ```

    VB
    ```
    Imports IMu
    ' ⋯
    Session.DefaultHost = "server.com"
    Session.DefaultPort = 12345
    Dim session = New Session
    session.Connect()
    ```

    This technique is useful when planning to create several connections to the same server or when wanting to get a [Handler](#2-1-handlers) object to create the connection automatically.

<h2 id="2-1-handlers">Handlers</h2>

Once a connection to an IMu server has been established, it is possible to create handler objects to submit requests to the server and receive responses.

> **NOTE:**
>
> When a handler object is created, a corresponding object is created by the IMu server to service the handler’s requests.

All handlers are subclasses of IMu’s `Handler` class.

> **NOTE:**
>
> You do not typically create a Handler object directly but instead use a subclass.

In this document we examine the most frequently used handler, `Module`, which allows you to find and retrieve records from a single EMu module.

<h1 id="3-accessing-an-emu-module">Accessing an EMu Module</h1>

The IMu API provides facilities to search, sort and retrieve information from records in any EMu module. This section contains the reference material for these facilities.

<h2 id="3-1-searching-a-module">Searching a Module</h2>

A program accesses an EMu module (or table, the terms are used interchangeably) using the `Module` class. The name of the table to be accessed is passed to the `Module` constructor. For example:

C#
```
using IMu;
// ⋯
Module parties = new Module("eparties", session);
```

VB
```
Dim parties = New IMu.Module("eparties", session)
```

> **Note:**
>
> The IMu class name `Module` conflicts with a Visual Basic reserved word and it is therefore necessary to use the fully qualified name `IMu.Module`.

This code assumes that a `Session` object called *session* has already been created. If a Session object is not passed to the `Module` constructor, a session will be created automatically using the `DefaultHost` and `DefaultPort` class properties. See [Connecting to an IMu server](#2-connecting-to-an-imu-server) for details.

Once a `Module` object has been created, it can be used to search the specified module and retrieve records.

Any one of the following methods can be used to search for records within a module:

* [findKey](#3-1-1-the-findkey-method)
* [findKeys](#3-1-2-the-findkeys-method)
* [findTerms](#3-1-3-the-findterms-method)
* [findWhere](#3-1-4-the-findwhere-method)

<h3 id="3-1-1-the-findkey-method">The FindKey Method</h3>

The `FindKey` method searches for a single record by its key. The key is a long integer (i.e. `long` in C#, `Long` in VB).

For example, the following code searches for a record with a key of 42 in the Parties module:

C#
```
using IMu;
// ⋯
Module parties = new Module("eparties", session);
long hits = parties.FindKey(42);
```

VB
```
Dim parties = New IMu.Module("eparties", session)
Dim hits = parties.FindKey(42)
```

The method returns the number of matches found, which is either `1` if the record exists or `0` if it does not.

<h3 id="3-1-2-the-findkeys-method">The FindKeys Method</h3>

The `FindKeys` method searches for a set of key values. The keys are passed as an array:

C#
```
using IMu;
// ⋯
Module parties = new Module("eparties", session);
long[] keys = { 52, 42, 17 };
long hits = parties.FindKeys(keys);
```

VB
```
Dim parties = New IMu.Module("eparties", session)
Dim keys() As Long = {52, 42, 17}
Dim hits = parties.FindKeys(keys)

```

or as a `List`:

C#
```
Module parties = new Module("eparties", session);
List<long> keys = new List<long>();
keys.Add(52);
keys.Add(42);
keys.Add(17);
long hits = parties.FindKeys(keys);
```

VB
```
Dim parties = New IMu.Module("eparties", session)
Dim keys New List(Of Long)
keys.Add(1)
keys.Add(2)
keys.Add(3)
Dim hits = parties.FindKeys(keys)
```

The method returns the number of records found.

<h3 id="3-1-3-the-findterms-method">The FindTerms Method</h3>

The `FindTerms` method is the most flexible and powerful way to search for records within a module. It can be used to run simple single term queries or complex multi-term searches.

The terms are specified using a `Terms` object. Once a `Terms` object has been created, add specific terms to it (using the `Add` method) and then pass the `Terms` object to the `FindTerms` method. For example, to specify a Parties search for records which contain a first name of “John” and a last name of “Smith”:

C#
```
Terms search = new Terms();
search.Add("NamFirst", "John");
search.Add("NamLast", "Smith");
// ⋯
long hits = parties.FindTerms(search);
```

VB
```
Dim search = New Terms
search.Add("NamFirst", "John")
search.Add("NamLast", "Smith")
' ⋯
Dim hits = parties.FindTerms(search)
```

There are several points to note:

1. 
    The first argument passed to the `Add` method element contains the name of the column or an alias in the module to be searched.
1. 
    An alias associates a supplied value with one or more actual columns. Aliases are created using the `AddSearchAlias` or `AddSearchAliases` methods.
1. 
    The second argument contains the value to search for.
1. 
    Optionally, a comparison operator can be supplied as a third argument (see below examples). The operator specifies how the value supplied as the second argument should be matched.

    Operators are the same as those used in TexQL (see KE’s [TexQL documentation](https://emu.kesoftware.com/downloads/Texpress/texql.pdf) for details). If not supplied, the operator defaults to “matches”.

    This is not a real TexQL operator, but is translated by the search engine as the most “natural” operator for the type of column being searched. For example, for *text* columns “matches” is translated as the `contains` TexQL operator and for *integer* columns it is translated as the `=` TexQL operator.

> **NOTE:**
>
>  Unless it is really necessary to specify an operator, consider using the `matches` operator, or better still supplying no operator at all as this allows the server to determine the best type of search.

**Examples**

1. 
    To search for the name “Smith” in the last name field of the Parties module, the following term can be used:

    C#
    ```
    Terms search = new Terms();
    search.Add("NamLast", "Smith");
    ```

    VB
    ```
    Dim search = New Terms
    search.Add("NamLast", "Smith")
    ```

1. 
    Specifying search terms for other types of columns is straightforward. For example, to search for records inserted on April 4, 2011:

    C#
    ```
    Terms search = new Terms();
    search.Add("AdmDateInserted", "Apr 4 2011");
    ```

    VB
    ```
    Dim search = New Terms
    search.Add("AdmDateInserted", "Apr 4 2011")
    ```

1. 
    To search for records inserted before April 4, 2011, it is necessary to add an operator:

    C#
    ```
    Terms search = new Terms();
    search.Add("AdmDateInserted", "Apr 4 2011", "<");
    ```

    VB
    ```
    Dim search = New Terms
    search.Add("AdmDateInserted", "Apr 4 2011", "<")
    ```

1. 
    By default, the relationship between the terms is a Boolean `AND`. This means that to find records which match both a first name containing “John” and a last name containing “Smith” the `Terms` object can be created as follows:

    C#
    ```
    Terms search = new Terms();
    search.Add("NamFirst", "John");
    search.Add("NamLast", "Smith");
    ```

    VB
    ```
    Dim search = New Terms
    search.Add("NamFirst", "John")
    search.Add("NamLast", "Smith")
    ```

1. 
    IMuTerms object where the relationship between the terms is a Boolean `OR` can be created by passing the string value “OR” to the `Terms` constructor:

    C#
    ```
    Terms search = new Terms(TermsKind.OR);
    search.Add("NamFirst", "John");
    search.Add("NamLast", "Smith");
    ```

    VB
    ```
    Dim search = New Terms(TermsKind.OR)
    search.Add("NamFirst", "John")
    search.Add("NamLast", "Smith")
    ```

    This specifies a search for records where either the first name contains “John” or the last name contains “Smith”.

1. 
    Combinations of `AND` and `OR` search terms can be created. The `AddAnd` method adds a new set of `AND` terms to the original `Terms` object. Similarly the `AddOr` method adds a new set of `OR` terms. For example, to restrict the search for a first name of “John” and a last name of “Smith” to matching records inserted before April 4, 2011 or on May 1, 2011, specify:

    C#
    ```
    Terms search = new Terms();
    search.Add("NamFirst", "John");
    search.Add("NamLast", "Smith");

    Terms dates = search.AddOr();
    dates.Add("AdmDateInserted", "Apr 4 2011", "<");
    dates.Add("AdmDateInserted", "May 1 2011");
    ```

    VB
    ```
    Dim search = New Terms
    search.Add("NamFirst", "John")
    search.Add("NamLast", "Smith")

    Dim dates = search.AddOr()
    dates.Add("AdmDateInserted", "Apr 4 2011", "<")
    dates.Add("AdmDateInserted", "May 1 2011")
    ```

1. 
    To run a search, pass the `Terms` object to the `FindTerms` method:

    C#
    ```
    Module parties = new Module("eparties", session);
    Terms search = new Terms();
    search.Add("NamLast", "Smith");
    long hits = parties.FindTerms(search);
    ```

    VB
    ```
    Dim parties = New IMu.Module("eparties", session)
    Dim search = New Terms
    search.Add("NamFirst", "John")
    Dim hits = parties.FindTerms(search)
    ```

    As with other find methods, the return value contains the estimated number of matches.

1. 
    To use a search alias, call the `AddSearchAlias` method to associate the alias with one or more real column names before calling `FindTerms`.
    
    Suppose we want to allow a user to search the Catalogue module for keywords. Our definition of a keywords search is to search the *SummaryData*, *CatSubjects_tab* and *NotNotes* columns. We could do this by building an `OR` search:

    C#
    ```
    string keyword = "⋯";
    // ⋯
    Terms search = new Terms(TermsKind.OR);
    search.Add("SummaryData", keyword);
    search.Add("CatSubjects_tab", keyword);
    search.Add("NotNotes", keyword);
    ```

    VB
    ```
    Dim keyword As String = "⋯"
    ' ⋯
    Dim search = New Terms(TermsKind.OR)
    search.Add("SummaryData", keyword)
    search.Add("CatSubjects_tab", keyword)
    search.Add("NotNotes", keyword)
    ```

    Another way of doing this is to register the association between the name *keywords* and the three columns we are interested in and then pass the name *keywords* as the column to be searched:

    C#
    ```
    string keyword = "⋯";
    // ⋯
    Module catalogue = new Module("ecatalogue", session);
    string[] columns =
    {
        "SummaryData",
        "CatSubjects_tab",
        "NotNotes"
    };
    catalogue.AddSearchAlias("keywords", columns);
    // ⋯
    Terms search = new Terms();
    search.Add("keywords", keyword);
    catalogue.FindTerms(search);
    ```

    VB
    ```
    Dim keyword As String = "⋯"
    ' ⋯
    Dim catalogue = new IMu.Module("ecatalogue", session)
    Dim columns() As String =
    {
        "SummaryData",
        "NamRoles_tab",
        "NotNotes"
    }
    catalogue.AddSearchAlias("keywords", columns)
    ' ⋯
    Dim search = New Terms
    search.Add("keywords", keyword)
    catalogue.FindTerms(search)
    ```

    An alternative to passing the columns as an array of strings is to pass a single string, with the column names separated by semi-colons:

    C#
    ```
    string keyword = "⋯";
    // ⋯
    Module catalogue = new Module("ecatalogue", session);
    string columns = "SummaryData;CatSubjects_tab;NotNotes";
    catalogue.AddSearchAlias("keywords", columns);
    // ⋯
    Terms search = new Terms();
    search.Add("keywords", keyword);
    catalogue.FindTerms(search);
    ```

    VB
    ```
    Dim keyword As String = "⋯"
    ' ⋯
    Dim catalogue = new IMu.Module("ecatalogue", session)
    Dim columns = "SummaryData;CatSubjects_tab;NotNotes"
    catalogue.AddSearchAlias("keywords", columns)
    ' ⋯
    Dim search = New Terms
    search.Add("keywords", keyword)
    catalogue.FindTerms(search)
    ```

    The advantage of using a search alias is that once the alias is registered a simple name can be used to specify a more complex `OR` search.

1. 
    To add more than one alias at a time, build an associative array of names and columns and call the `AddSearchAliases` method:

    C#
    ```
    Map aliases = new Map();
    aliases.Add("keywords", "SummaryData;CatSubjects_tab;NotNotes");
    aliases.Add("title", "SummaryData;TitMainTitle");
    catalogue.AddSearchAliases(aliases);
    ```

    VB
    ```
    Dim aliases = New Map
    aliases.Add("keywords", "SummaryData;CatSubjects_tab;NotNotes")
    aliases.Add("title", "SummaryData;TitMainTitle")
    catalogue.AddSearchAliases(aliases)
    ```

<h3 id="3-1-4-the-findwhere-method">The FindWhere Method</h3>

With the `FindWhere` method it is possible to submit a complete TexQL *where* clause:

C#
```
Module parties = new Module("eparties", session);
string where = "NamLast contains 'Smith'";
long hits = parties.FindWhere(where);
```

VB
```
Dim parties = New IMu.Module("eparties", session)
Dim where = "NamLast contains 'Smith'"
Dim hits = parties.FindWhere(where)
```

Although this method provides complete control over exactly how a search is run, it is generally better to use `FindTerms` to submit a search rather than building a where clause. There are (at least) two reasons to prefer `FindTerms` over `FindWhere`:

1. 
    Building the *where* clause requires the code to have detailed knowledge of the data type and structure of each column. The `FindTerms` method leaves this task to the server.

    For example, specifying the term to search for a particular value in a nested table is straightforward. To find Parties records where the Roles nested table contains Artist, `FindTerms` simply requires:
    
    ```
    $search->add('NamRoles_tab', 'Artist');
    ```

    On the other hand, the equivalent TexQL clause is:

    ```
    exists(NamRoles_tab where NamRoles contains 'Artist');
    ```

    The TexQL for double nested tables is even more complex.

1. 
    More importantly, findTerms is more secure.

    With `FindTerms` a set of terms is submitted to the server which then builds the TexQL *where* clause. This makes it much easier for the server to check for terms which may contain SQL-injection style attacks and to avoid them.
    
    If your code builds a *where* clause from user-entered data so it can be run using `FindWhere`, it is much more difficult, if not impossible, for the server to check and avoid SQL-injection. The responsibility for checking for SQL-injection becomes yours.

<h3 id="3-1-5-number-of-matches">Number of Matches</h3>

All of the *find* methods return the number of matches found by the search. For `FindKey` and `FindKeys` this number is always the exact number of matches found. The number returned by `FindTerms` and `FindWhere` is best thought of as an estimate.

This estimate is almost always correct but because of the nature of the indexing used by the server’s data engine (Texpress) the number can sometimes be an over-estimate of the real number of matches. This is similar to the estimated number of hits returned by a Google search.

<h2 id="3-2-sorting">Sorting</h2>

<h3 id="3-2-1-the-sort-method">The sort Method</h3>

The `Module` class `Sort` method is used to order a set of matching records once the search of a module has been run.

<h4 id="3-2-1-1-arguments">Arguments</h4>

This `Sort` method takes two arguments:

* **columns**

    The *columns* argument is used to specify the columns by which to sort the result set. The argument can be either a `string`, `string[]` or a `List<string>`. Each string can be a simple column name or a set of column names, separated by semi-colons or commas.

    Each column name can be preceded by a `+` (plus) or `-` (minus or dash). A leading `+` indicates that the records should be sorted in ascending order. A leading `-` indicates that the records should be sorted in descending order.

    > **NOTE:**
    >
    > If a sort order (`+` or `-`) is not given, the sort order defaults to ascending.

* **flags**

    The *flags* argument is used to pass one or more flags to control the way the sort is carried out. As with the *columns* argument, the *flags* argument can be a `string`, `string[]` or a `List<string>`. Each `string` can be a single flag or a set of flags separated by semi-colons or commas.

    The following flags control the type of comparisons used when sorting:

    * **word-based**

        Sort disregards white spaces (more than the one space between words). As of Texpress 9.0, `word-based` sorting no longer disregards punctuation. For example:
    
        > Traveler's&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Inn

        will be sorted as

        > Traveler's Inn

    * **full-text**

        Sort includes all punctuation and white spaces. For example:

        > Traveler's&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Inn

        will be sorted as

        > Traveler's&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Inn

        and will therefore differ from

        > Traveler's Inn

    * **compress-spaces**
    
        Sort includes punctuation but disregards all white space (with the exception of a single space between words). For example:

        > Traveler's&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Inn

        will be sorted as

        > Traveler's Inn

        > **NOTE:**
        >
        > If none of these flags are included, the comparison defaults to word-based.

    The following flags modify the sorting behaviour:

    * **case-sensitive**

        Sort is sensitive to upper and lower case. For example:

        > Melbourne gallery

        will be sorted separately to

        > Melbourne Gallery

    * **order-insensitive**

        Values in a multi-value field will be sorted alphabetically regardless of the order in which they display. For example, a record which has the following values in the *NameRoles_tab* column in this order:

        1. Collection Manager
        1. Curator
        1. Internet Administrator

        and another record which has the values in this order:

        1. Internet Administrator
        1. Collection Manager
        1. Curator

        will be sorted the same.

    * **null-low**

        Records with empty columns will be placed at the start of the result set rather than at the end.

    * **extended-sort**

        Values that include diacritics will be sorted separately to those that do not. For example:

        > entrée

        will be sorted separately to

        > entree

    The following flags can be used when generating a summary of the sorted records:

    * **report**

        A summary of the sort is generated. The summary report is a hierarchically structured object that summarises the number of unique values contained in the sort columns. See [Return Value](#3-2-1-2-return-value) and [Example](#3-2-1-3-example) for a description and illustration of the returned structure.

    * **table-as-text**

        All data from multi-valued columns will be treated as a single value (joined by line break characters) in the summary results array. For example, for a record which has the following values in the NamRoles_tab column:

        > Collection Manager, Curator, Internet Administrator

        the summary will include statistics for a single value:

        > Collection Manager
        > Curator
        > Internet Administrator

        Thus the number of values in the summary results display will match the number of records.

        If this option is not included, each value in a multi-valued column will be treated as a distinct value in the summary. Thus there may be many more values in the summary results than there are records.

For example:

1. 
    Sort parties by first name (ascending):

    C#
    ```
    Module parties = new Module("eparties", session);
    Terms search = new Terms();
    search.Add("NamLast", "Smith");
    long hits = parties.FindTerms(search);
    parties.Sort("NamFirst");
    ```

    VB
    ```
    Dim parties = new IMu.Module("eparties", session)
    Dim search = New Terms
    search.Add("NamLast", "Smith")
    Dim hits = parties.FindTerms(search)
    parties.Sort("NamFirst")
    ```

1. 
    Sort parties by title (ascending) and then first name (descending):

    C#
    ```
    Module parties = new Module("eparties", session);
    Terms search = new Terms();
    search.Add("NamLast", "Smith");
    parties.FindTerms(search);
    string[] columns =
    {
        "NamTitle",
        "-NamFirst"
    };
    parties.Sort(sort);
    ```

    VB
    ```
    Dim parties = new IMu.Module("eparties", session)
    Dim search = New Terms
    search.Add("NamLast", "Smith")
    parties.FindTerms(search)
    Dim columns() =
    {
        "NamTitle",
        "-NamFirst"
    }
    parties.Sort(sort)
    ```

1. 
    Run a case-sensitive sort of parties by title (ascending) and then first name (descending):

    C#
    ```
    Module parties = new Module("eparties", session);
    Terms search = new Terms();
    search.Add("NamLast", "Smith");
    parties.FindTerms(search);
    string[] columns =
    {
        "NamTitle",
        "-NamFirst"
    };
    string[] flags =
    {
        "case-sensitive"
    };
    parties.Sort(sort, flags);
    ```

    VB
    ```
    Dim parties = new IMu.Module("eparties", session)
    Dim search = New Terms
    search.Add("NamLast", "Smith")
    parties.FindTerms(search)
    Dim columns() =
    {
        "NamTitle",
        "-NamFirst"
    }
    Dim columns() =
    {
        "case-sensitive"
    }
    parties.Sort(sort, flags)
    ```

<h4 id="3-2-1-2-return-value">Return Value</h4>

The `Sort` method returns `null` unless the *report* flag is used.

If the *report* flag is used, the `Sort` method returns a `ModuleSortResult` object. This object contains two read-only properties.

* **Count**

    An `int` specifying the number of distinct terms in the primary sort key.

* **Terms**

    A `ModuleSortTerm[]` containing the list of distinct terms associated with the primary key in the sorted result set.

This `ModuleSortTerm` object contains three read-only properties which describe the term:

* **Value**
    
    A `String` that is the distict value itself.

* **Count**

    A `long` specifying the number of records in the result set which have the value.

* **Nested**

    A `ModuleSortResult` object that holds the values for secondary sorts within the primary sort.

This is illustrated in the following example.

<h4 id="3-2-1-3-example">Example</h4>

This example shows a three-level sort by title, last name (descending) and first name on a set of Parties records:

```
using System;
using IMu;

class Program
{
    static void Main(string[] args)
    {
        Session session = new Session("imu.mel.kesoftware.com", 40136);
        Module parties = new Module("eparties", session);

        Terms terms = new Terms(TermsKind.OR);
        terms.Add("NamLast", "Smith");
        terms.Add("NamLast", "Wood");

        long hits = parties.FindTerms(terms);

        String[] sort =
        {
            "NamTitle",
            "-NamLast",
            "NamFirst"
        };
        String[] flags = 
        {
            "full-text",
            "report"
        };
        ModuleSortResult report = parties.Sort(sort, flags);
        showSummary(report, 0);
        Environment.Exit(0);
    }

    private static void showSummary(ModuleSortResult report, int indent)
    {
        String prefix = "";
        for (int i = 0; i < indent; i++)
            prefix += "  ";

        ModuleSortTerm[] terms = report.Terms;
        for (int i = 0; i < terms.Length; i++)
        {
            ModuleSortTerm term = terms[i];

            String value = term.Value;
            long count = term.Count;
            Console.WriteLine("{0}{1} ({2})", prefix, value, count);

            ModuleSortResult nested = term.Nested;
            if (nested != null)
                showSummary(nested, indent + 1);
        }
    }
}
```

This displays the distinct terms (and their counts) for the primary sort key (title). Nested under each primary key is the set of distinct terms and counts for the secondary key (last name) and nested under each secondary key is the set of distinct terms and counts for the tertiary key (first name):

```
Mr (2)
  Wood (1)
    Gerard (1)
  SMITH (1)
    Ian (1)
Ms (1)
  ECCLES-SMITH (1)
    Kate (1)
Sir (1)
  Wood (1)
    Henry (1)
 (3)
  Wood (1)
    Grant (1)
  Smith (2)
    Sophia (1)
    William (1)
```

If another sort key was specified its terms would be nested under the tertiary key and so on.

> **NOTE:**
>
> In the example above some of the records do not have a value for the primary sort key (title). By default these values are sorted after any other values. They can be sorted before other values using the null-low flag.

<h2 id="3-3-getting-information-from-matching-records">Getting Information from Matching Records</h2>

<h3 id="3-3-1-the-fetch-method">The fetch Method</h3>

The `Module` class [Fetch](TODO-link-to-reference) method is used to get information from the matching records once the search of a module has been run. The server maintains the set of matching records in a list and the `Fetch` method can be used to retrieve any information from any contiguous block of records in the list.

#### Arguments

The `Fetch` method has four arguments:

* **flag**

* **offset**

    Together the *flag* and *offset* arguments define the starting position of the block of records to be fetched. The *flag* argument is a string and must be one of:

    * `start`
    * `current`
    * `end`

    The `start` and `end` flags refer to the first record and the last record in the matching set. The “current” flag refers to the position of the last record fetched by the previous call to the `Fetch` method. If the `Fetch` method has not been called, `current` refers to the first record in the matching set.

    The *offset* argument is an integer. It adjusts the starting position relative to the value of the *flag* argument. A positive value for *offset* specifies a start after the position specified by *flag* and a negative value specifies a start before the position specified by *flag*.

    For example, calling `Fetch` with a *flag* of `start` and *offset* of `3` will return records starting from the fourth record in the matching set. Specifying a *flag* of `end` and an *offset* of `-8` will return records starting from the ninth last record in the matching set.

    To retrieve the next record after the last returned by the previous `Fetch`, you would pass a *flag* of `current` and an *offset* of `1`.

* **count**

    The *count* argument specifies the maximum number of records to be retrieved.

    Passing a count value of `0` is valid. This causes `Fetch` to change the current record without actually retrieving any data.

    Using a negative value for *count* is also valid. This causes `Fetch` to return all the records in the matching set from the starting position (specified by *flag* and *offset*).

* **columns**

    The optional columns argument is used to specify which columns should be included in the returned records. The argument can be either a `String`, `String[]` or a `List<string>`. In its simplest form each `String` contains a single column name, or several column names separated by semi-colons or commas.

    The value of the columns argument can be more than simple column names. See the section on [Specifying Columns](#3-3-2-Specifying-Columns) for details.

For example:

1. 
    Retrieve the first record from the start of a set of matching records:

    C#
    ```
    Module parties = new Module("eparties", session);
    string columns = "NamFirst;NamLast";
    ModuleFetchResult result = parties.Fetch("start", 0, 1, columns);
    ```

    VB
    ```
    Dim parties = New IMu.Module("eparties", session)
    Dim columns = "NamFirst;NamLast"
    Dim result = parties.Fetch("start", 0, 1, columns)
    ```

    The *columns* argument can also be specified as an array reference:

    C#
    ```
    Module parties = new Module("eparties", session);
    string[] columns =
    {
        "NamFirst",
        "NamLast"
    };
    ModuleFetchResult result = parties.Fetch("start", 0, 1, columns);
    ```

    VB
    ```
    Dim parties = New IMu.Module("eparties", session)
    Dim columns() =
    {
        "NamFirst",
        "NamLast"
    }
    Dim result = parties.Fetch("start", 0, 1, columns)
    ```

1. 
    Return all of the results in a matching set:

    C#
    ```
    Module parties = new Module("eparties", session);
    string[] columns =
    {
        "NamFirst",
        "NamLast"
    };
    ModuleFetchResult result = parties.Fetch("start", 0, -1, columns);
    ```

    VB
    ```
    Dim parties = New IMu.Module("eparties", session)
    Dim columns() =
    {
        "NamFirst",
        "NamLast"
    }
    Dim result = parties.Fetch("start", 0, -1, columns)
    ```

1. 
    Change the current record to the next record in the set of matching records without retrieving any data:

    C#
    ```
    parties.Fetch("start", 1, 0);
    ```

    VB
    ```
    parties.Fetch("start", 1, 0)
    ```

1. 
    Retrieve the last record from the end of a set of matching records:

    C#
    ```
    Module parties = new Module("eparties", session);
    string[] columns =
    {
        "NamFirst",
        "NamLast"
    };
    ModuleFetchResult result = parties.Fetch("end", 0, 1, columns);
    ```

    VB
    ```
    Dim parties = New IMu.Module("eparties", session)
    Dim columns() =
    {
        "NamFirst",
        "NamLast"
    }
    Dim result = parties.Fetch("end", 0, 1, columns)
    ```

#### Return Value


The `Fetch` method returns records requested in an [ModuleFetchResult](TODO-link-to-reference) object. It contains three members:

* **Count**

    The number of records returned by the `Fetch` request.

* **Hits**

    The estimated number of matches in the result set. This number is returned for each `Fetch` because the estimate can decrease as records in the result set are processed by the `Fetch` method.

* **Rows**

    The Rows property is an array containing the set of records requested. Each element of the Rows array is itself a Map object. Each Map object contains entries for each column requested.
    
    > **Note:**
    >
    >The IMu `Map` class is a subclass of .Net’s standard `Dictionary`. It defines its key type to be a string. It also provides some convenience methods for converting the types of elements stored in the map. See Reference for details.

For example, retrieve the *Count* & *Hits* properties and iterate over all of the returned records using the *rows* property:

C#
```
String[] columns =
{
    "NamFirst",
    "NamLast"
};

ModuleFetchResult result = parties.Fetch("start", 0, 2, columns);
long count = result.Count;
long hits = result.Hits;

Console.WriteLine("Count: {0}", count);
Console.WriteLine("Hits: {0}", hits);
Console.WriteLine("Rows:");
foreach (IMu.Map row in result.Rows)
{
    int rowNum = row.GetInt("rownum");
    long irn = row.GetInt("irn");
    String first = row.GetString("NamFirst");
    String last = row.GetString("NamLast");

    Console.WriteLine("  {0}. {1}, {2} ({3})", rowNum, last, first,
            irn);
}
```

VB
```
// TODO
```

This will produce output similar to the following:

```
Count: 2
Hits: 4
Rows:
  1. ECCLES-SMITH, Kate (100573)
  2. SMITH, Ian (100301)
```

<h3 id="3-3-2-specifying-columns">Specifying Columns</h3>

This section specifies the values that can be included or used as the columns arguments to the `Module` class `Fetch` method.

<h4 id="3-3-2-1-atomic-columns">Atomic Columns</h4>

These are simple column names of the type already mentioned, for example:

```
NamFirst
```

The values of atomic columns are returned as strings:

C#
```
string[] columns =
{
    "NamFirst"
};
ModuleFetchResult result = parties.Fetch("start", 0, -1, columns);
for (int i = 0; i < rows.Length; i++)
{
    Map row = rows[i];
    string first = row.GetString("NamFirst");
    // ⋯
}
```

VB
```
Dim columns() =
{
    "NamFirst"
}
Dim result = parties.Fetch("start", 0, -1, columns)
For i = 0 To rows.Length - 1
    Dim row = rows(i)
    Dim first = row.GetString("NamFirst")
    ' ⋯
Next
```

<h4 id="3-3-2-2-nesting-tables">Nested Tabes</h4>

Nested tables are columns that contain a list of values. They are specified similarly to atomic columns:

```
NamRoles_tab
```

The values of nested tables are returned as an array. Each array member is a string that corresponds to a row from the nested table:

C#
```
string[] columns =
{
    "NamRoles_tab"
};
ModuleFetchResult result = parties.Fetch("start", 0, -1, columns);
    IMu.Map[] rows = result.Rows;
    foreach (IMu.Map row in rows)
{
    String[] roles = row.GetStrings("NamRoles_tab");
    foreach (String role in roles)
    {
        // ⋯
    }
}
```

VB
```
Dim columns() =
{
    "NamRoles_tab"
}
Dim result = parties.Fetch("start", 0, -1, columns)
For i = 0 To rows.Length - 1
    Dim row = rows(i)
    Dim first = row.GetString("NamFirst")
    ' ⋯
Next
```

<h4 id="3-3-2-3-columns-from-attached-records">Columns From Attached Records</h4>

An attachment is a link between a record in a module and a record in the same or another module. The columns from an attached record can be specified by first specifying the attachment column and then the column to retrieve from the attached record:

```
SynSynonymyRef_tab.SummaryData
```

Multiple columns can be specified from the attached record:

```
SynSynonymyRef_tab.(NamFirst,NamLast)
```

The return values of columns from attached records depends on the type of the attachment column. If the attachment column is atomic then the column values are returned as an associative array. If the attachment column is a nested table the values are returned as an array. Each array member is an associative array containing the requested column values for each attached record:

C#
```
String[] columns =
{
    "SynSynonymyRef_tab.(NamFirst,NamLast)"
};
ModuleFetchResult result = partiesFetch("start", 0, 1, columns);
foreach (IMu.Map row in result.Rows)
{
    foreach (IMu.Map synonymy in row.GetMaps("SynSynonymyRef_tab"))
    {
        String first = synonymy.GetString("NamFirst");
        String last = synonymy.GetString("NamLast");
        // ⋯
    }
}
```

VB
```
// TODO
```

<h4 id="3-3-2-4-columns-from-reverse-attachments">Columns from Reverse Attachments</h4>

A reverse attachment allows you to specify columns from other records in the same module or other modules that have the current record attached to a specified column.

For example:

1. 
    Retrieve the *TitMainTitle* (Main Title) column for all Catalogue records that have the current Parties record attached to their *CreCreatorRef_tab* (Creator) column:

    ```
    <ecatalogue:CreCreatorRef_tab>.TitMainTitle
    ```

1. 
    Retrieve the *NarTitle* (Title) column for all Narratives records that have the current Narrative record attached to their *HieChildNarrativesRef_tab* (Child Narratives) column:

    ```
    <enarratives:HieChildNarrativesRef_tab>.NarTitle
    ```

Multiple columns can be specified from the reverse attachment record:

```
<ecatalogue:CreCreatorRef_tab>.(TitMainTitle,TitObjectCategory)
```

Reverse attachments are returned as an array. Each array member is an associative array containing the requested column values from each record from the specified module (The Catalogue module in the example below) that has the current record attached to the specified column (The *CreCreatorRef_tab* column in the example below):

C#
```
String[] columns =
{
    "<ecatalogue:CreCreatorRef_tab>.(TitMainTitle,TitObjectCategory)"
};
ModuleFetchResult result = parties.Fetch("start", 0, 1, columns);
foreach (IMu.Map row in result.Rows)
{
    foreach (IMu.Map object in row.GetMaps("ecatalogue:CreCreatorRef_tab"))
    {
        String title = object.GetString("TitMainTitle");
        String category = object.GetString("TitObjectCategory");
        // ⋯
    }
}
```

VB
```
// TODO
```

<h4 id="3-3-2-5-grouped-nested-tables">Grouped Nested Tables</h4>

A set of nested table columns can be grouped by specifying them between square brackets.

For example, to group the Contributors and their Role from the Narratives module:

```
[NarContributorRef_tab.SummaryData,NarContributorRole_tab]
```

Each corresponding rows of the supplied nested tables are returned as a single table row in the returned results. By default, the group is given a name of *group1*, *group2* and so on. This group name can be changed by prefixing the grouped columns with an alternative name:

```
contributors=[NarContributorRef_tab.SummaryData,NarContributorRole_tab]
```

The grouped nested tables are returned as an array. Each array member is an associative array containing corresponding rows from the nested tables:

C#
```
String[] columns =
{
    "[NarContributorRef_tab.SummaryData,NarContributorRole_tab]"
};
ModuleFetchResult result = narratives-Fetch('start', 0, 1, columns);
foreach (IMu.Map row in result.Rows)
{
    foreach (IMu.Map group in row.GetMaps("group1"))
    {
        IMu.Map contributor = group.GetMap("NarContributorRef_tab");
        String summary = contributor.GetString("SummaryData");
        String role = group.GetString("NarContributorRole_tab");
        // ⋯
    }
}
```

VB
```
// TODO
```

<h4 id="3-3-2-6-virtual-columns">Virtual Columns</h4>

Virtual columns are columns that do not actually exist in the EMu table being accessed. Instead, the IMu server interprets the request for the column and builds an appropriate response. Certain virtual columns can only be used in certains modules as follows:

**The following virtual columns can be used in any EMu module:**

* insertedTimeStamp

    Returns the insertion date and time of the record using the format `YYY-MM-DDThh:mm:ss`, for example "1999-12-31T23:59:59". This is similar to the [ISO8601](http://en.wikipedia.org/wiki/ISO_8601) date format except the time zone designator is not included.

* modifiedTimeStamp

    Returns the modification date and time of the record using the format `YYYY-MM-DDThh:mm:ss`.

    C#
    ```
    String[] columns =
    {
        "insertedTimeStamp",
        "modifiedTimeStamp"
    };
    ModuleFetchResult result = parties.Fetch("start", 0, 1, columns);
    foreach (IMu.Map row in result.Rows)
    {
        String inserted = row.GetString("insertedTimeStamp");
        String modified = row.GetString("modifiedTimeStamp");
        // ⋯
    }
    ```

    VB
    ```
    // TODO
    ```

**The following virtual columns can be used in any EMu module except Multimedia:**

* application

    Returns information about the preferred [application](GLOSSARY.md###-Application) multimedia attached to a record.

    > **NOTE:**
    >
    > Currently the preferred multimedia is the same as the first entry in the list returned by the *applications* virtual column. However, future versions of EMu may allow other multimedia to be flagged as preferred, in which case the *application* column will return information for the preferred multimedia, rather than the first one.

* applications

    Returns information about all of the application multimedia attached to a record.

* audio

    Returns information about the preferred [audio](GLOSSARY.md###-Audio) multimedia attached to a record.

* audios

    Returns information about all of the audio multimedia attached to a record.

* image

    Returns information about the preferred [image](GLOSSARY.md###-Image) multimedia attached to a record.

* images

    Returns information about all of the image multimedia attached to a record.

* multimedia

    Returns information about all of the multimedia attached to a record.

* video

    Returns information about the preferred [video](GLOSSARY.md###-Video) multimedia attached to a record.

* videos

    Returns information about all of the video multimedia attached to a record.

See [Multimedia](#3-4-multimedia) for more information.

**The following virtual columns can only be used in the Multimedia module:**

* master
    
    Returns information about the [master](GLOSSARY.md###-Master) multimedia file.

* resolutions

    Returns information about all multimedia [resolutions](GLOSSARY.md###-Resolutions).

* resource

    Returns minimal information about the master multimedia file including an open file handle to a temporary copy of the multimedia file.

* resource

    Returns minimal information about the master multimedia file including an open file handle to a temporary copy of the multimedia file.

* resources

    The same as the 8resource* virtual column except that information and file handles are supplied for all multimedia files.

* supplementary

    Returns information about all [supplementary](GLOSSARY.md###-Supplementary) multimedia files.

* thumbnail

    Returns information about the multimedia [thumbnail](GLOSSARY.md###-Thumbnail).

See [Multimedia](#3-4-multimedia) for more information.

**The following virtual column can only be used in the Narratives module:**

* trails

    Returns information about the position of current Narratives record in the narratives hierarchy.

**The following virtual column can only be used in the Collection Descriptions module:**

* extUrlFull_tab

<h4 id="3-3-2-7-fetch-sets">Fetch Sets</h4>

A fetch set allows you to pre-register a group of columns by a single name. That name can then be passed to the `Fetch` method to retrieve the specified columns.

Fetch sets are useful if the `Fetch` method will be called several times with the same set of columns because:

* The required columns do no have to be specified every time the `Fetch` method is called. This is useful when [maintaining state](#4-maintaining-state).

* Every time the `Fetch` method is called the IMu server must parse the supplied columns and check them against the EMu schema. For complex column sets, particularly those involving several references or reverse references, this can take time.

The `Module` class `AddFetchSet` method is used to register a set of columns. This method takes two arguments:

* **name**

    The name to use for the column set. The value of this argument can be passed to any call to the `Fetch` method and the set of columns specified by the *columns* argument will be returned.

* **columns**

    The set of columns to be associated with the *name* argument.

The `Module` class `AddFetchSets` method is similar except that multiple sets can be registered at one time.

The results are returned as if you had supplied the columns directly to the `Fetch` method.

For example:

1. 
    Add a single fetch set using the `AddFetchSet` method:

    C#
    ```
    String[] columns =
    {
        "NamFirst",
        "NamLast",
        "NamRoles_tab"
    };
    parties.AddFetchSet('person_details', $columns);
    ```

    VB
    ```
    // TODO
    ```

1. 
    Add multiple fetch sets using the `AddFetchSets` method:

    C#
    ```
    IMu.Map sets = new IMu.Map();
    sets.Add("person_details",
        new String[]{ "NamFirst", "NamLast", "NamRoles_tab" });
    sets.Add("organisation_details",
        new String[]{ "NamOrganisation", "NamOrganisationAcronym" });
    parties.AddFetchSets(sets);
    ```

    VB
    ```
    // TODO
    ```

1. 
    Retrieve a fetch set using the `Fetch` method:

    C#
    ```
    ModuleFetchResult result = parties.Fetch(Sstart", 0, 1, "person_details");
    foreach (IMu.Map row in result.Rows)
    {
        String first = row.GetString("NamFirst");
        String last = row.GetString("NamLast");

        String[] roles = row.GetStrings("NamRoles_tab");
        foreach (String role in roles)
        {
            // ⋯
        }
    }
    ```

    VB
    ```
    // TODO
    ```

    > **WARNING:**
    >
    >The fetch set name must be the **only** value passed as the `Fetch` method *columns* argument. This may be revised in a future version of the IMu API.

<h4 id="3-3-2-8-renaming-columns">Renaming columns</h4>

Columns can be renamed in the returned results by prefixing them with an alternative name:

```
first_name=NamFirst
```

The value of the specified column is now returned using the alternative name:

C#
```
String[] columns =
{
    "first_name=NamFirst"
};
ModuleFetchResult result = parties.Fetch("start", 0, 1, $columns);
foreach (IMu.Map row in result.Rows)
{
    String first = row.GetString"first_name");
    // ⋯
}
```

VB
```
// TODO
```

Alternative names can be supplied to other column specifications as well:

```
roles=NamRoles_tab
```

```
synonyms=SynSynonymyRef_tab.(NamFirst,NamLast)
```

```
objects=<ecatalogue:CreCreatorRef_tab>.(TitMainTitle,TitObjectCategory)
```

Alternative names can also be used for any columns specified between round brackets or square brackets:

```
SynSynonymyRef_tab.(first_name=NamFirst,last_name=NamLast)
```

```
contributors=[contributor=NarContributorRef_tab.SummaryData,role=NarContributorRole_tab]
```

Alternative names can also be supplied in fetch sets:

C#
```
String[] columns =
{
    "first_name=NamFirst",
    "last_name=NamLast",
    "roles=NamRoles_tab"
};
parties.AddFetchSet("person_details", columns);
```

VB
```
// TODO
```

<h3 id="3-3-3-example">Example</h3>

In this example we build a simple .Net based web page to search the Parties module by last name and display the full set of results.

```
using System;
using IMu;

namespace a_simple_example
{
    class Program
    {
        static void Main(string[] args)
        {
            string host = "oxford.man.kesoftware.com";//"imu.mel.kesoftware.com";
            int port = 40136;

            Session session = new Session(host, port);
			// start-example
            Module parties = new Module("eparties", session);

            Terms terms = new Terms();
            terms.Add("NamLast", "Smith");
            long hits = parties.FindTerms(terms);

            String[] columns =
            {
                "NamFirst",
                "NamLast"
            };
            ModuleFetchResult result = parties.Fetch("start", 0, 2, columns);
			// end-example
            int count = result.Count;
            IMu.Map[] rows = result.Rows;

            Console.WriteLine("Count: {0}", count);
            Console.WriteLine("Hits: {0}", hits);
            Console.WriteLine("Rows:");
            foreach (IMu.Map row in rows)
            {
                int rowNum = row.GetInt("rownum");
                long irn = row.GetLong("irn");
                String firstName = row.GetString("NamFirst");
                String lastName = row.GetString("NamLast");
                Console.WriteLine(" {0}. {1}, {2} ({3})",
                    rowNum, lastName, firstName, irn);
            }
            Console.WriteLine("Press any key to finish...");
            Console.ReadKey();
        }
    }
}
```

In this example the *name* parameter entered via the HTML search page is submitted to the .Net script. The script searches for parties records that have the entered value as a last name and display the parties first and last names in an HTML table.

<h2 id="3-4-multimedia">Multimedia</h2>

The IMu API provides a number of special mechanisms to handle access to the multimedia stored in the EMu <abbr title="Database management system">DBMS</abbr>. These machanisms fall into three rough categories:

1. 
    Mechanisms to select Multimedia module records that are attached to another module. This is covered in the [Multimedia Attachments](#3-4-1-multimedia-attachments) section.
1. 
    Mechanisms to select multimedia files from a Multimedia module record. This is covered in the [Multimedia Files](#3-4-2-multimedia-files) and [Filters](#3-4-3-filters) sections.
1. 
    Mechanisms to apply modifications to multimedia files. This is covered in the [Modifiers](#3-4-4-modifiers) section.

It is important to note that a single record in the EMu DBMS can have multiple Multimedia module records associated with it. Each Multimedia module record can have multiple multimedia files associated with it. The seperate mechanisms for handling multimedia access can be composed so that it is possible to, for example:

* Select a specific Multimedia module record from a group of attached Multimedia module records.
* Select a specific multimedia file from the selected Multimedia record.
* Apply a modification to the selected multimedia file.

<h3 id="3-4-1-multimedia-attachments">Multimedia Attachments</h3>

Information about the multimedia attached to an EMu record from any module (**except** the Multimedia module itself) can be retrieved using the `Module` class `Fetch` method by specifying one of the following [virtual columns](#3-3-2-6-virtual-columns).

The following virtual columns return information about a single multimedia attachment of the current record. The information is returned as a associative array:

* *application*
* *audio*
* *image*
* *video*

The following virtual columns return information about a set of multimedia attachments of the current record. The information is returned as an array. Each array member is an associative array containing the information for a single multimedia attachment from the set:

* *applications*
* *audios*
* *images*
* *multimedia*
* *videos*

All of these virtual columns return the [irn](GLOSSARY.md###-IRN), [type](GLOSSARY.md###-MIME-type) and [format](GLOSSARY.md###-MIME-format) of the Multimedia record attached to the current record. They also act as reference columns to the Multimedia module. This means that other columns from the Multimedia module (including [virtual columns](#3-3-2-6-virtual-columns)) can also be requested from the corresponding Multimedia record, for example:

1. 
    Include the title for all attached multimedia:

    ```
    multimedia.MulTitle
    ```

1. 
    Include the title for all attached images:

    ```
    images.MulTitle
    ```

1. 
    Include details about the master multimedia file for all attached images (using the virtual Multimedia module column master):

    ```
    images.master
    ```

1. 
    Include multiple columns for all attached images:

    ```
    images.(master,MulTitle,MulDescription)
    ```

1. 
    Include and rename multiple columns for all attached images:

    ```
    images.(master,title=MulTitle,description=MulDescription)
    ```

<h4 id="3-4-1-1-example">Example</h4>

This example shows the retrieval of the base information and the title for all multimedia images attached to a parties record:

C#
```
String[] columns = 
{
    "images.MulTitle"
};
ModuleFetchResult result = parties.Fetch("start", 0, 1, columns);
foreach (IMu.Map row in result.Rows)
{
    foreach (IMu.Map image in row.GetMaps("images"))
    {
        int irn = image.GetInt("irn");
        String type = image.GetString("type");
        String format = image.GetString("format");
        String title = image.GetString("MulTitle");

        Console.WriteLine("irn {0}: {1} - {2}/{3}",
            irn, title, type, format);
    }
}
```

VB
```
// TODO
```

This will produce output similar to the following:

```
irn 100105: Signature of Luciano Pavarotti - image/jpeg
irn 100096: Luciano Pavarotti - image/gif
irn 100100: Luciano Pavarotti with Celine Dion - image/gif
irn 100101: Luciano Pavarotti with Natalie Cole - image/gif
irn 100102: Luciano Pavarotti with the Spice Girls - image/gif
```

<h3 id="3-4-2-multimedia-files">Multimedia Files</h3>

Similarly, information about the multimedia files associated with a Multimedia module record can be retrieved using the `Module` class `Fetch` method by specifying one of the following [virtual columns](#3-3-2-6-virtual-columns).

The following virtual columns return information about a single multimedia file from the current Multimedia record. The information is returned as a associative array.

* *master*
* *resource*
* *thumbnail*

The following virtual columns return information about a set of multimedia files of the current Multimedia record. The information is returned as an array. Each array member is a associative array containing the information for a single multimedia file from the set:

* *resolutions*
* *resources*
* *supplementary*

The master, thumbnail, resolutions and supplementary virtual columns all return the same type of information. That information differs for image and non-image multimedia as follows:

For non-image multimedia they return:

* *fileSize*

    The size of the file in bytes.

* *identifier*

    The name of the multimedia file.

* *index*

    An integer that specifies the multimedia files position in the list of the master, thumbnail, resolutions and supplementary (in that order) multimedia files numbered from 0.

* *kind*

    The kind (`master`, `thumbnail`, `resolution`, or `supplementary`) of the multimedia.

* *md5Checksum*

    The [MD5](http://en.wikipedia.org/wiki/MD5) checksum of the multimedia file.

* *md5Sum*

    The same as *md5Checksum* (included for backwards compatibility).

* *mimeFormat*

    The media format.

* *mimeType*

    The media type.

* *size*

    The same as *fileSize* (included for backwards compatibility).

For image multimedia they return all of the values specified for non-image multimedia and also include:

* *bitsPerPixel*

    The colour depth of the image.

* colourSpace

    The colour space of the image.

* compression

    The type of compression used on the image.

* height

    The height of the image in pixels.

* imageType

    The type classification of the image. For example:

    * *Bilevel*: Specifies a monochrome image.
    
    * *ColorSeparation*: Specifies a grayscale image.

    * *Grayscale*: Specifies a grayscale image.

    * *GrayscaleMatte*: Specifies a grayscale image with opacity.

    * *Palette*: Specifies a indexed color (palette) image.

    * *PaletteMatte*: Specifies a idexed color (palette) image with opacity.

    * *TrueColor*: Specifies a truecolor image.

    * *TrueColorMatte*: Specifies a truecolor image with opacity.

    Some more information can be found [here](http://www.imagemagick.org/Magick++/Enumerations.html#ImageType)

* *numberColours*

    The number of colours in the image.

* *numberPages*

    The number of images within the main image - a feature that is supported only in certain file types, e.g. TIFF.

* *planes*

    The number of planes in an image.

* *quality*

    An integer value from 1 to 100 that indicates the quality of the image. A lower value indicates a lower image quality and higher compression and a higher value indicates a higher image quality but a lower compression. Only applicable to JPEG and MPEG image formats.

* *resolution*

    The resolution of the image in <abbr title="Pixels per inch">PPI</abbr>.

* *width*

    The width of the image in pixels.

The resource and resources virtual columns both return the same type of information as follows:

* *identifier*

    The name of the multimedia file.

* *mimeType*

    The media type.

* *mimeFormat*

    The media format.

* *size*

    The size of the file in bytes.

* *file*

    A [File Stream](https://docs.microsoft.com/en-us/dotnet/api/system.io.filestream). This provides a read-only handle to a temporary copy of the multimedia file. The temporary copy of the file is discarded when the handle is closed or destroyed.

    > **NOTE:**
    >
    > If the resource column is specified with a filter, a modifier must also be provided in order for the file handle to be returned, eg:
    >
    > ```
    > 'resources(height @ 200){resource:include}'
    > ```
    > 
    > Modifier options include:
    >
    > 1. resource:include - (will add the file handle to the data set returned)
    > 1. resource:only - (will replace the data set returned with the file handle)

* *height*

    The height of the image in pixels.

* *width*

    The width of the image in pixels.

<h4 id="3-4-2-1-example">Example</h4>

This example shows the retrieval of the multimedia title and resource information about all multimedia files for all multimedia images attached to a parties record:

C#
```
String[] columns = 
{
    "images.(MulTitle, resources)"
};
ModuleFetchResult result = parties.Fetch("start", 0, 1, columns);
foreach (IMu.Map row in result.Rows)
{
    foreach (IMu.Map image in row.GetMaps("images"))
    {
        int irn = image.GetInt("irn");
        String type = image.GetString("type");
        String format = image.GetString("format");
        String title = image.GetString("MulTitle");

        Console.WriteLine("irn {0}: {1} - {2}/{3}",
            irn, title, type, format);
        foreach (IMu.Map resource in image.GetMaps("resources"))
        {
            int height = resource.GetInt("height");
            String identifier = resource.GetString("identifier");
            String mimeFormat = resource.GetString("mimeFormat");
            String mimeType = resource.GetString("mimeType");
            int size = resource.GetInt("size");
            int width = resource.GetInt("width");

            Console.WriteLine("  {0}: {1}/{2} - {3}x{4} - {5} bytes",
                identifier, mimeType, mimeFormat, height, width, size);
        }
        Console.WriteLine();
    }
}
```

VB
```
// TODO
```

This will produce output similar to the following:

```
irn 100105: Signature of Luciano Pavarotti - image/jpeg
  signature.jpg: image/jpeg - 85x300 - 6535 bytes
  signature.thumb.jpg: image/jpeg - 25x90 - 1127 bytes

irn 100096: Luciano Pavarotti - image/gif
  LucianoPavarotti.gif: image/gif - 400x273 - 19931 bytes
  LucianoPavarotti.thumb.jpg: image/jpeg - 90x61 - 1354 bytes
  LucianoPavarotti.300x300.jpg: image/jpeg - 300x205 - 41287 bytes

irn 100100: Luciano Pavarotti with Celine Dion - image/gif
  PavarottiWithCelineDion.gif: image/gif - 400x381 - 66682 bytes
  PavarottiWithCelineDion.thumb.jpg: image/jpeg - 90x85 - 2393 bytes
  PavarottiWithCelineDion.300x300.jpg: image/jpeg - 300x286 - 76091 bytes

irn 100101: Luciano Pavarotti with Natalie Cole - image/gif
  PavarottiWithNatalieCole.gif: image/gif - 251x400 - 44551 bytes
  PavarottiWithNatalieCole.thumb.jpg: image/jpeg - 56x90 - 1768 bytes
  PavarottiWithNatalieCole.300x300.jpg: image/jpeg - 188x300 - 49698 bytes

irn 100102: Luciano Pavarotti with the Spice Girls - image/gif
  PavarottiWithSpiceGirls.gif: image/gif - 326x400 - 64703 bytes
  PavarottiWithSpiceGirls.thumb.jpg: image/jpeg - 73x90 - 2294 bytes
  PavarottiWithSpiceGirls.300x300.jpg: image/jpeg - 245x300 - 65370 bytes
```
The actual bytes of the multimedia file can be accessed using the file handle from the file value returned using the resource or resources virtual columns. We can use the file handle to copy the file from the IMu server:

C#
```
String[] columns = 
{
    "image.resource{resource:inclue}"
};
ModuleFetchResult result = parties.Fetch("start", 0, 1, columns);
foreach (IMu.Map row in result.Rows)
{
    IMu.Map image = row.GetMap("image");
    IMu.Map resource = image.GetMap("resource");
    
    String name = resource.GetString("identifier");
    FileStream temp = (FileStream) resource["file"];

    FileStream copy = new FileStream(name, FileMode.OpenOrCreate,
        FileAccess.Write);
    byte[] buffer = new byte[4096]; // 4k buffer
    int length = 0;

    while ((length = temp.Read(buffer, 0, 4096)) > 0)
    {
        copy.Write(buffer, 0, length);
    }
    copy.Close();
    temp.Close();
}
```

VB
```
// TODO
```

This copies the multimedia file from the IMu server to a local file with the same name, in this case *signature.jpg*

<h3 id="3-4-3-filters">Filters</h3>

While the Multimedia module virtual columns provide a reasonably fine-grained method for selecting specific multimedia files associated with a multimedia record, in some circumstances it is useful to have even more control over the selection of multimedia files, particularly when specifying the *resolutions*, *resources* or *supplementary* virtual columns.

Filters provide a mechanism to specify particular files associated with a multimedia record based on certain characteristics of the files. Filters consist of three parts; a name, a operator and a value. They are specified in round brackets after a virtual column:

```
column(name operator value);
```

Multiple values can be specified by separating each filter with a comma:

```
column(name operator value, name operator value);
```

* **name**

    The filter name specifies the characteristic of the multimedia file to filter on. Unless noted otherwise the meaning of the filter names is as specified in [Multimedia Files](#3-4-2-multimedia-files) section.

    The following filter names can be used to filter any multimedia file:

    * *fileSize* (or size)
    * *height*
    * *identifier*
    * *index*
    * *kind*
    * *mimeFormat* (or format)
    * *mimeType* (or type)
    * *width*

    The following filter names can be used to filter multimedia image files:

    * *bitsPerPixel*
    * *colourSpace*
    * *compression*
    * *imageType*
    * *md5Checksum* (or md5sum)
    * *numberColours*
    * *numberPages*
    * *planes*
    * *quality*
    * *resolution*

    The following filter name can be used to filter supplementary multimedia files:

    * *usage*

        The value of the supplementary attributes usage (SupUsage_tab) column.

* **operator**

    The operator specifies how the filter value should relate to the multimedia file characteristic specified by the filter *name*. The available values are:

    * == 

    Equals.

    Selects the multimedia files where the characteristic specified by the filter *name* is the same as the filter *value*.

    * !=

    Not equals.

    Selects the multimedia files where the characteristic specified by the filter *name* is not the same as the filter *value*.

    * <

    Less than.

    Selects the multimedia files where the characteristic specified by the filter *name* is less than the filter *value*. Only applies to numeric values.

    * \>

    Greater than.

    Selects the multimedia files where the characteristic specified by the filter *name* is greater than the filter *value*. Only applies to numeric values.

    * <=

    Less than or equal to.

    Selects the multimedia files where the characteristic specified by the filter *name* is less that or equal to the filter *value*. Only applies to numeric values.

    * \>=

    Greater than or equal to.

    Selects the multimedia files where the characteristic specified by the filter *name* is greater that or equal to the filter *value*. Only applies to numeric values.

    * @

    Closest to (also called best fit).

    Selects the single multimedia file where the characteristic specified by the filter *name* is closest to the filter *value*. Only applies to numeric values.

    * ^

    Closest to but greater than.

    Selects the single multimedia file where the characteristic specified by the filter *name* is closest to but greater than the filter *value*. Only applies to numeric values.

* value

    The value to filter by. Any value can be used but, obviously, only certain values make sense for each filter. For example, if the fileSize filter is being used then only a numeric value is useful. Similarly, if the mimeType filter is being used then only a text value that corresponds to a valid MIME type is useful.

For example:

1. 
    Select multimedia resolutions with a width greater that 300 pixels:

```
resolutions(width > 300)
```

1. 
    Select the single multimedia resource with a width closest to 600:

```
resources(width @ 600)
```

1. 
    Select the thumbnail resource:

```
resources(kind == thumbnail)
```

1. 
    Specify multiple filters to select the single multimedia resource with a width and height closest to 600:

```
resources(width @ 600, height @ 600)
```

<h4 id="3-4-3-1-example">Example</h4>

This example shows the retrieval of the multimedia title and resource information about the single multimedia file with a width closest to 300 for all multimedia images attached to a parties record:

C#
```
String[] columns =
{
    "images.(MulTitle, resources(width @ 300))"
};
ModuleFetchResult result = parties.Fetch("start", 0, 1, columns);
foreach (IMu.Map row in result.Rows)
{
    foreach (IMu.Map image in row.GetMaps("images"))
    {
        int irn = image.GetInt("irn");
        String type = image.GetString("type");
        String format = image.GetString("format");
        String title = image.GetString("MulTitle");

        Console.WriteLine("irn {0}: {1} - {2}/{3}",
            irn, title, type, format);

        foreach (IMu.Map resource in image.GetMaps("resources"))
        {
            long height = resource.GetInt("height");
            String identifier = resource.GetString("identifier");
            String mimeFormat = resource.GetString("mimeFormat");
            String mimeType = resource.GetString("mimeType");
            long size = resource.GetInt("size");
            long width = resource.GetInt("width");

            Console.WriteLine("  {0}: {1}/{2} - {3}x{4} - {5} bytes",
                identifier, mimeType, mimeFormat, height, width, size);
        }
        Console.WriteLine();
    }
}
```

VB
```
// TODO
```

> **NOTE:**
>
> The only difference from the previous example is the inclusion of a filter on the resources Multimedia virtual column.

This will produce output similar to the following:

```
irn 100105: Signature of Luciano Pavarotti - image/jpeg
  signature.jpg: image/jpeg - 85x300 - 6535 bytes

irn 100096: Luciano Pavarotti - image/gif
  LucianoPavarotti.gif: image/gif - 400x273 - 19931 bytes

irn 100100: Luciano Pavarotti with Celine Dion - image/gif
  PavarottiWithCelineDion.300x300.jpg: image/jpeg - 300x286 - 76091 bytes

irn 100101: Luciano Pavarotti with Natalie Cole - image/gif
  PavarottiWithNatalieCole.300x300.jpg: image/jpeg - 188x300 - 49698 bytes

irn 100102: Luciano Pavarotti with the Spice Girls - image/gif
  PavarottiWithSpiceGirls.300x300.jpg: image/jpeg - 245x300 - 65370 bytes
```

<h3 id="3-4-4-modifiers">Modifiers</h3>

While the IMu API provides a number of ways to select particular multimedia files from a Multimedia record sometimes none of the available files fulfill the required characteristics. Sometimes it is necessary to modify an existing multimedia file to achieve the desired result.

Modifiers provide a mechanism to convert multimedia images returned by the IMu server in a number of ways. The modifications are performed on-the-fly and do **not** affect the multimedia stored in the Multimedia database; they only apply to the temporary copy of multimedia returned by the IMU API. Modifiers consist of two parts; a name and a value seperated by a colon. They are specified in braces (curly brackets) after a *resource* or *resources* virtual column:

```
column{name:value}
```

Multiple values can be specified by seperating each filter with a comma:

```
column(…){name:value}
```

The supported values for name are:

* *checksum*

    Include a checksum value with the resource (or resources) virtual column response. While this does not actually apply any modifications to a multimedia file it is useful when you require a checksum for multimedia that has had a modifier applied to it (cf. original multimedia).

    The allowed value parts are:

    * *crc32*

    Include a [CRC32](http://en.wikipedia.org/wiki/Cyclic_redundancy_check) checksum.

    * *md5*

    Include a [MD5](http://en.wikipedia.org/wiki/Md5) checksum.

    When the checksum modifier is used the resource (or resources) virtual column response includes a checksum component.

* *format*

    Specifies that the multimedia file should be converted to the specified [format](GLOSSARY.md###-MIME-format). If the multimedia is not already in the required format it is reformatted on-the-fly.

    The IMu server uses ImageMagick to process the image and the range of supported formats is very large. The complete list is available from: http://www.imagemagick.org/script/formats.php. Any of the supported formats can be used as the value part of this modifier.

* *resource*

    Specifies that a multimedia file handle should be returned.

    The allowed value parts are:

    * `include`
    * `only`

* *height*

    Specifies that the multimedia image file should be converted to the specified height (in pixels). If the Multimedia record contains a resolution with this height, this resolution is returned instead (i.e. no modification is applied). Otherwise the closest matching larger resolution is resized to the requested height on-the-fly.

    The allowed *value* parts are any numeric value specifying the height in pixels.

* *width*

    Specifies that the multimedia image file should be converted to the specified width (in pixels). If the Multimedia record contains a resolution with this width, this resolution is returned instead (i.e. no modification is applied). Otherwise the closest matching larger resolution is resized to the requested width on-the-fly.

    The allowed *value* parts are any numeric value specifying the width in pixels.

* *aspectratio*

    Controls whether the image’s aspect ratio should be maintained when both a height and a width modifier are specified. If set to no, the aspect ratio is not maintained. by default the aspect ratio is maintained.

    The allowed value parts are:

    * `yes`
    * `no`

> **NOTE:**
>
> Modifiers currently only apply to multimedia images and can only be specified after the Multimedia virtual *resource* or *resources* columns.
>
> Only the *resource* or *resources* parts of the returned results are affected by modifiers. By design, all other response parts include the information for the original, unmodified multimedia.

For example:

1. 
    Specify a Base64 encoding modifier:

```
resource{encoding:base64}
```

1. 
    Include a CRC32 checksum in the response:

```
resource{checksum:crc32}
```

1. 
    Reformat the multimedia image to the gif format:

```
resource{format:gif}
```

1. 
    Resize the multimedia image to a height of 300 pixels:

```
resource{height:300}
```

1. 
    Resize the multimedia image to a width of 300 pixels:

```
resource{width:300}
```

1. 
    Resize the multimedia image to a height & width of 300 pixels and do not maintain aspect ratio:

```
resource{height:300, width:300, aspectratio:no}
```

#### Performance Issues

Modifying a multimedia file is computationally expensive, it should only be used when absolutely necessary. For example, it is better to use the filtering mechanism to select multimedia image files of the desired dimensions rather than modifying them to fit:

Good:

```
resource(height @ 300, width @ 300)
```

Not so good:

```
resource{height:300, width:300)
```

Obviously this only works if you have image file resolutions that are close to the desired dimensions.

Modifying a multimedia image file that is closer to the desired dimensions is less computationally expensive than modifying a larger image, so selecting the appropriate image prior to modification is preferable:

Good:

```
resource(height ^ 299, width ^ 299){height:300, width:300}
```

Not so good:

```
resource{height:300, width:300}
```

#### Example

This example shows the retrieval of the multimedia title and setting a *format*, *width* & *height* modifier to the resource information for the master multimedia image attached to a narratives record:

C#
```
Terms terms = new Terms();
terms.Add("NarTitle", "Angus Young");

long hits = narratives.FindTerms(terms);

String[] columns =
{
    "image.(MulTitle, resource{format:jpeg, height:600, width:600, checksum:md5})"
};
ModuleFetchResult result = narratives.Fetch("start", 0, 1, columns);

foreach (IMu.Map row in result.Rows)
{
    IMu.Map image = row.GetMap("image");
    int irn = image.GetInt("irn");
    String type = image.GetString("type");
    String format = image.GetString("format");
    String title = image.GetString("MulTitle");

    Console.WriteLine("irn {0}: {1} - {2}/{3}",
        irn, title, type, format);

    IMu.Map resource = image.GetMap("resource");
    long height = resource.GetInt("height");
    String identifier = resource.GetString("identifier");
    String mimeFormat = resource.GetString("mimeFormat");
    String mimeType = resource.GetString("mimeType");
    long size = resource.GetInt("size");
    long width = resource.GetInt("width");

    Console.WriteLine("  {0}: {1}/{2} - {3}x{4} - {5} bytes",
        identifier, mimeType, mimeFormat, height, width, size);               
}
```

VB
```
// TODO
```

This will produce output similar to the following:

```
irn 165: Angus Young, AC/DC Jacket - image/tiff
  00033320.jpeg: image/jpeg - 599x401 - 77396 bytes - d94a9f46bd6274bcd20154bc513cf61f
```

The bytes of the modified multimedia can be accessed in the usual way via the resource response file value.

> **NOTE:**
>
> * Only the *resource* response part has been affected by the modifier. The *image* response part still reports the *format* as *tiff*. This is by design.
>
> * Because the aspect ratio has been maintained the image does not have the exact height and width specified.

<h1 id="4-maintaining-state">Maintaining State</h1>

One of the biggest drawbacks of the [earlier example](#3-3-3-example) is that it fetches the full set of results at one time, which is impractical for large result sets. It is more practical to display a full set of results across multiple pages and allow the user to move forward or backward through the pages.

This is simple in a conventional application where a connection to the separate server is maintained until the user terminates the application. In a web implementation however, this seemingly simple requirement involves a considerably higher level of complexity due to the stateless nature of web pages. One such complexity is that each time a new page of results is displayed, the initial search for the records must be re-executed. This is inconvenient for the web programmer and potentially slow for the user.

The IMu server provides a solution to this. When a handler object is created, a corresponding object is created on the server to service the handler’s request: this server-side object is allocated a unique identifier by the IMu server. When making a request for more information, the unique identifier can be used to connect a new handler to the same server-side object, with its state intact.

The following example illustrates the connection of a second, independently created `Module` object to the same server-side object:

C#
```
// Create a module object as usual
Module first = new Module("eparties", session);

// Run a search - this will create a server-side object
int[] keys = { 1, 2, 3, 4, 5, 42 };
first.FindKeys(keys);

// Get a set of results
ModuleFetchResult result1 = first.Fetch("start", 0, 2, "SummaryData");

// Create a second module object
Module second = new Module("eparties", session);

/*
 * Attach it to the same server-side object as the first module. This is
 * the key step.
 */
second.ID = first.ID;

// Get a second set of results from the same search
ModuleFetchResult result2 = second.Fetch("current", 1, 2, "SummaryData");
```

VB
```
// TODO
```

Although two completely separate `Module` objects have been created, they are each connected to the same server-side object by virtue of having the same `ID` property. This means that the second `Fetch` call will access the same result set as the first `Fetch`. Notice that a flag of *current* has been passed to the second call. The current state is maintained on the server-side object, so in this case the second call to `Fetch` will return the third and fourth records in the result set.

While this example illustrates the use of the `ID` property, it is not particularly realistic as it is unlikely that two distinct objects which refer to the same server-side object would be required in the same piece of code. The need to re-connect to the same server-side object when generating another page of results is far more likely. This situation involves creating a server-side `Module` object (to search the module and deliver the first set of results) in one request and then re-connecting to the same server-side object (to fetch a second set of results) in a second request. As before, this is achieved by assigning the same identifier to the `ID` property of the object in the second page, but two other things need to be considered.

By default the IMu server destroys all server-side objects when a session finishes. This means that unless the server is explicitly instructed not to do so, the server-side object may be destroyed when the connection from the first page is closed. Telling the server to maintain the server-side object only requires that the `Destroy` property on the object is set to false before calling any of its methods. In the example above, the server would be instructed not to destroy the object as follows:

C#
```
Module module = new Module("eparties", session);
module.SetDestroy(false);
int[] keys = { 1, 2, 3, 4, 5, 42 };
module.FindKeys(keys);
```

VB
```
// TODO
```

The second point is quite subtle. When a connection is established to a server, it is necessary to specify the port to connect to. Depending on how the server has been configured, there may be more than one server process listening for connections on this port. Your program has no control over which of these processes will actually accept the connection and handle requests. Normally this makes no difference, but when trying to maintain state by re-connecting to a pre-existing server-side object, it is a problem.

For example, suppose there are three separate server processes listening for connections. When the first request is executed it connects, effectively at random, to the first process. This process responds to the request, creates a server-side object, searches the Parties module for the terms provided and returns the first set of results. The server is told not to destroy the object and passes the server-side identifier to another page which fetches the next set of results from the same search.

The problem comes when the next page connects to the server again. When the connection is established any one of the three server processes may accept the connection. However, only the first process is maintaining the relevant server-side object. If the second or third process accepts the connection, the object will not be found.

The solution to this problem is relatively straightforward. Before the first request closes the connection to its server, it must notify the server that subsequent requests need to connect explicitly to that process. This is achieved by setting the `Session` object’s `Suspend` property to *true* prior to submitting any request to the server:

C#
```
Session session = new Session("server.com", 12345);
Module module = new Module("eparties", session);
session.SetSuspend(true);
module.FindKeys(keys);
```

VB
```
// TODO
```

The server handles a request to suspend a connection by starting to listen for connections on a second port. Unlike the primary port, this port is guaranteed to be used only by that particular server process. This means that a subsequent page can reconnect to a server on this second port and be guaranteed of connecting to the same server process. This in turn means that any saved server-side object will be accessible via its identifier. After the request has returned (in this example it was a call to `FindKeys`), the `Session` object’s `Port` property holds the port number to reconnect to:

C#
```
session.SetSuspend(true);
module.FindKeys(keys);
int reconnect = session.Port;
```

VB
```
// TODO
```

<h2 id="4-1-example">Example</h2>

To illustrate we’ll modify the very simple results page of the [earlier section](#3-3-3-example) to display the list of matching names in blocks of five records per page. We’ll provide simple *Next* and *Prev* links to allow the user to move through the results, and we will use some more `GET` parameters to pass the port we want to reconnect to, the identifier of the server-side object and the rownum of the first record to be displayed.

1. 
    Create a `Session` object with parameters `Host` and `Port`, then establish a connection and immediately set the `Suspend` property to true to tell the server that we may want to connect again:

    ```
    Session session = new Session(Host, Port);
    session.Connect();
    session.Suspend = true;
    ```

    This ensures the server listens on a new, unique port.
    
1. 
    Create the client-side `Module` object and set its destroy property to *false*:

    ```
    Module parties = new Module("eparties", session);
    parties.Destroy = false;
    ```

    This ensures that the server will not destroy the corresponding server-side object when the session ends.

1. 
    If we have search terms, we will perform a new search. If connecting to a new server-side module we record the current `Module` `ID` property, otherwise we set the `ID` property:

    ```
    parties.Destroy = false;

    if (ID == null)
        ID = parties.ID;
    else
        parties.ID = ID;
    ```

1. 
    Build a list of columns to fetch:

    ```
    static private String[] columns =
    {
        "NamFirst",
        "NamLast"
    };
    ```

1. 
    If the URL included a *rownum* parameter, fetch records starting from there. Otherwise start from record number *1*:

    ```
    parties.ID = ID;
    ```

    After this, we display the results. For the sake of our console example, we manually disconnect from the server, as we do not need to ‘refresh’ like a web browser.


1. 
    Finally, we allow the user to move forwards and backwards through the results. To do this we need to pass on the information about our connection, current position and number of hits.

    ```
    Map value = new Map();
    value.Add("ID", parties.ID);
    value.Add("Hits", results.Hits);
    value.Add("rownum", rownum);
    return value;
    ```

    We then start a loop, prompting the user to move forward/backwards between results or to quit:

    ```
    input = Console.ReadKey();
    if (input.Key == ConsoleKey.Q)
        break;

    if (input.Key == ConsoleKey.LeftArrow)
    {
        rownum -= count;
        if (rownum < 1)
            rownum = 1;
    }
    else if (input.Key == ConsoleKey.RightArrow)
    {
        if (rownum + count < Hits)
            rownum += count;
    }
    else
        continue;

    result = ServerRequest(ID, rownum);
    rownum = result.GetInt("rownum");
    Hits = result.GetLong("Hits");

    ```

<h1 id="5-logging-in-to-an-imu-server">Logging in to an IMu server</h1>

When an IMu based program connects to an IMu server it is given a default level of access to EMu modules.

It is possible for an IMu based program to override this level of access by explicitly logging in to the server as a registered user of EMu. This is done by using the `Session‘s` `login` method. Once the `login` method has been called successfully the session remains authenticated until the `logout` method is called.

<h2 id="5-1-the-login-method">The login method</h2>

The login method is used to authenticate the program as a registered user of EMu. Once successfully authenticated access to EMu modules is at the level of the authenticated user rather than the default imuserver user.

<h3 id="5-1-1-arguments">Arguments</h3>

* **username**

    The name of the user to login as. This must be the name of a registered EMu user on the system.

* **password**

    The user’s password. This argument is optional and if it is not supplied it defaults to `null`.

    > **NOTE:**
    >
    > Supplying a `null` password is uncommon but it is sometimes a valid thing to do. If the server receives a password of `null` it will try to authenticate the user using server-side methods such as verification against emu’s .rhosts file.

* **spawn**

    A boolean value indicating whether the IMu server should create a separate process dedicated to handling this program’s requests. This argument is optional and if not supplied it defaults to `true`.

<h2 id="5-2-the-logout-method">The logout method</h2>

The logout method relinquishes access as the previously authenticated user.

> **NOTE:**
>
> Logging in this way is very similar to logging into the same EMu environment using the EMu client. Access to records is controlled via record-level security.

> **WARNING:**
>
> Logging in causes the IMu server to start a new texserver process to handle all access to EMu module. This new texserver process will use a Texpress licence. The licence will not be freed until the logout method is called. See the server FAQ [How does IMu use Texpress licences?](FAQ.md##-How-does-imu-use-texpress-licences?) for more information.

<h1 id="6-updating-an-emu-module">Updating an EMu Module</h1>

The `Module` class provides methods for inserting new records and for updating or removing existing records in any EMu module.

> **NOTE:**
>
> By default these operations are restricted by the IMu server. Typically access to these operations is gained by [logging in to the IMu server](#5-1-The-login-method). See the [allow-updates](CONFIGURATION.md##allow-updates) entry of the server configuration for more information.

<h2 id="6-1-the-insert-method">The insert Method</h2>

The `Insert` method is used to add a new record to the module.

<h3 id="6-1-1-arguments">Arguments</h3>

The method takes two arguments:

* **values**

    The *values* argument specifes any data values to be inserted into the newly created record.

    The *values* should be an associative [Map](TODO-link-to-reference) object. The indexes of the `Map` must be column names.

* **columns**

    The *columns* argument is used to specify which columns should be returned once the record has been created. The value of the *column* is specified in exactly the same way as in the `fetch` method. See the section on [Specifying Columns](#3-3-2-Specifying-Columns) for details.

    > **NOTE:**
    >
    > It is very common to include `irn` as one of the columns to be returned. This gives a way of getting the key of the newly created record.

<h3 id="6-1-2-return-value">Return Value</h3>

The method returns a [Map](TODO-link-to-reference). This associative array contains an entry for each column requested.

<h3 id="6-1-3-example">Example</h3>

C#
```
Module parties = new Module("eparties", session);

/* Specify the values to insert.
*/
Map values = new Map();
values.Add("NamFirst", "Chris");
values.Add("NamLast", "Froome");
String[] otherNames =
{
    "Christopher",
    "Froomey",
};
values.Add("NamOtherNames_tab", otherNames);

/* Specify the column values to return after inserting.
*/
String[] columns =
{
    "irn",
    "NamFirst",
    "NamLast",
    "NamOtherNames_tab"
};

/* Insert the new record.
*/
Map result = null;
try
{
    result = parties.Insert(values, columns);
}
catch (IMuException e)
{
    Console.WriteLine("Error: {0}", e);
    Environment.Exit(1);
}

/* Output the returned values.
*/
int irn = result.GetInt("irn");
String first = result.GetString("NamFirst");
String last = result.GetString("NamLast");
String[] others = result.GetStrings("NamOtherNames_tab");

Console.WriteLine("{0}, {1} ({2})", last, first, irn);
Console.WriteLine("Other names:");
foreach (String other in others)
{
    Console.WriteLine("\t{0}", other);
}
Environment.Exit(0);
```

VB
```
// TODO
```

If inserting of records is permitted this will produce output similar to the following:

```
Froome, Chris (435)
Other names:
	Christopher
	Froomey
```

If inserting of records is denied by the server this will produce output similar to the following:

```
Error: ModuleUpdatesNotAllowed (authenticated,default) [500]
```

<h2 id="6-2-the-update-method">The update Method</h2>

The `Update` method is used to modify one or more existing records. This method operates very similarly to the `Fetch` method. The only difference is a *values* argument which contains a set of values to be applied to each specified record.

<h3 id="6-2-1-arguments">Arguments</h3>

The method takes five arguments:

* **flag**

* **offset**

* **count**

    These arguments are identical to those used by the [Fetch](#3-3-1-The-fetch-Method) method. They define the starting position and size of the block of records to be updated.

* **values**

    The *values* argument specifies the columns to be updated in the specified block of records. The *values* argument must be a [Map](TODO-link-to-reference). The keys of the `Map` object must be column names.

    This is the same as the values argument for the [Insert](#6-1-The-insert-Method) method.

* **columns**

    The *columns* argument is used to specify which columns should be returned once the record has been created. The value of the _column_ is specified in exactly the same way as in the `Fetch` method. See the section on [Specifying Columns](#3-3-2-Specifying-Columns) for details.

    This is the same as the _columns_ argument for the `Insert` method.

<h3 id="6-2-2-return-value">Return Value</h3>

The `Update` method returns an `ModuleFetchResult` object (the same
as the [Fetch](#3-3-1-The-fetch-Method) method). It contains
the values for the selected block of records after the updates have been
applied.

<h3 id="6-2-3-example">Example</h3>

C#
```
/* Find all parties records that have a first name of "Chris" and a
** last name of "Froome".
*/
Module parties = new Module("eparties", session);
Terms terms = new Terms();
terms.Add("NamFirst", "Chris");
terms.Add("NamLast", "Froome");
parties.FindTerms(terms);

/* Specify the column to update and the new value.
*/
Map values = new Map();
values.Add("NamFirst", "Christopher");

/* Specify the column values to return after updating.
*/
String[] columns =
{
    "irn",
    "NamFirst",
    "NamLast"
};

/* Run the update.
*/
ModuleFetchResult result = null;
try
{
    result = parties.Update("start", 0, -1, values, columns);
}
catch (IMuException e)
{
    Console.WriteLine("Error: {0}", e);
    Environment.Exit(1);
}

/* Output the returned values.
*/
Console.WriteLine("Count: {0}", result.Count);
Console.WriteLine("Hits: {0}", result.Hits);

Console.WriteLine("Rows:");
foreach (IMu.Map row in result.Rows)
{
    int rowNum = row.GetInt("rownum");
    int irn = row.GetInt("irn");
    String first = row.GetString("NamFirst");
    String last = row.GetString("NamLast");

    Console.WriteLine("\t{0}. {1}, {2} ({3})", rowNum, last, first,
        irn);
}
Environment.Exit(0);
```

VB
```
// TODO
```

If updating records is allowed the example will produce output similar to the following:

```
Count: 1
Hits: 1
Rows:
	1. Froome, Christopher (435)
```

<h2 id="6-3-the-remove-method">The remove Method</h2>

The `Remove` method is used to remove one or more existing records.

<h3 id="6-3-1-arguments">Arguments</h3>

The method takes three arguments:

* **flag**
* **offset**
* **count**

These arguments define the starting position and size of the block of records to be removed. They are identical to those used by the [Fetch](#3-3-1-The-fetch-Method) and [Update](#6-2-The-update-Method) methods.

<h3 id="6-3-2-return-value">Return Value</h3>

The method returns a `long` that specifies the number of records that were removed.


<h3 id="6-3-3-example">Example</h3>

C#
```
/* Find all parties records that have a first name of "Christopher" and
** a last name of "Froome".
*/
Module parties = new Module("eparties", session);
Terms terms = new Terms();
terms.Add("NamFirst", "Christopher");
terms.Add("NamLast", "Froome");
parties.FindTerms(terms);

/* Remove all of the matching records.
*/
long result = 0;
try
{
    result = parties.Remove("start", 0, -1);
}
catch (IMuException e)
{
    Console.WriteLine("Error: {0}", e);
    Environment.Exit(1);
}

/* Output the number of removed records.
*/
Console.WriteLine("Removed {0} record(s)", result);
Environment.Exit(0);
```

VB
```
// TODO
```

If removing records is allowed the example will produce output similar to the following:

```
Removed 1 record(s)
```

<h1 id="7-exceptions">Exceptions</h1>

When an error occurs, the IMu .Net API throws an exception. The exception is an [IMuException](TODO-link-to-reference) object. This is a subclass of .Net's standard `ApplicationException` class.

The `IMuException` class overrides the `Exception` classes `ToString` method and returns an error message.

To handle specific IMu errors it is necessary to check the exception is an `IMuException` object before using it. The `IMuException` class includes a property called `id`. This is a string and contains the internal IMu error code for the exception. For example, you may want to catch the exception raised when a `Session` objects `Connect` method fails and try to connect to an alternative server:

C#
```
String mainServer = "server1.com";
String alternativeServer = "server2.com";
Session session = new Session();
session.Host = mainServer;
try
{
    session.Connect();
}
catch (IMuException e)
{
    // Check for specific SessionConnect error
    if (e.ID != "SessionConnect")
    {
        Console.WriteLine("Error: {0}", e);
        return;
    }
    session.Host = alternativeServer;
    try
    {
        session.Connect();
    }
    catch (Exception e)
    {
        Console.WriteLine("Error: {0}", e);
        return;
    }
}
/*
 * By the time we get to here the session is connected to either the main
 * server or the alternative.
 */
```

VB
```
// TODO
```
