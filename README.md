# BinarySerializer_v5

New do not optimized on current time, version binary structure builder based on .IL NET (GrEmit)

### On now support
###### Basic C types (int, float, etc...)
###### String type
###### Struct types (DateTime, TimeSpan, Vector)
###### List, Dictinary, Array types
###### Nested class types

### Have types contains size

**BinaryList16<<T>>** - List (have 16 bit header lenght)

**BinaryList32<<T>>** - List (have 32 bit header lenght)

**BinaryString16** - String (have 16 bit header lenght)

**BinaryString32** - String (have 32 bit header lenght)

.. and other

### Or not contains size

**BinaryList<<T>>** - List (with no have pre lenght header)

**BinaryString** - String (with no have pre lenght header)

.. and other

For types not contains pre size header, builder have fixed size or "name property" propertyes for settup in attribute

### Performance

At now serializer can write ~x2.5 slower then with binary writer, and can read ~x2 faster read then with binary reader

### BinarySchemeAttribute

must used for propertyes set scheme, on propertyes which included in scheme name

BinarySchemeAttribute can set one time for property, and sets serialize type, and optional size propertyes for types how need this

### BinaryPreCompileAttribute

must used for classes we needed precompiled

BinaryPreCompileAttribute can set many time for class, need for pre compile struct for many schemes, and can set pre init buffer size

TypeStorage have function PreCompileBinaryStructs, working with Assembly.GetExecutingAssembly() you can compille all structs in current assembly

### At now 

Working with Nullable struct types

### In the future 

Create serialization scenarios with functions

Create settings for control insertion append, size check, nullable check and other builder constructions
