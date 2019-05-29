# BinarySerializer_v5

New do not optimized on current time, version binary structure builder based on .IL NET (GrEmit) and NET Core 2.0

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

### Working

For comfortable programming, text encoding is set in TypeStorage, we can using TypeStorage.Instance or create new sample TypeStorage for separate coding, or structs group

For create custom type you must override IBasicType interface, and use your type like the other default types

Type must have constructor with no parameters, public or protected access modificator

### Sample

    public class Sample
    {
      [Binary(typeof(BinaryList16<BinaryString16>))]
      [BinaryScheme("default")]
      public List<string> names { get;set;}
    } 

    Sample s = new Sample()
    {
      names = new List<string>()
      {
        "Tom",
        "Anna",
        "R2D2"
      }
    };

    BinarySerializer bs = new BinarySerializer(); // have constructor with parameter TypeStorage

    byte[] buffer = bs.Serialize("default", s):

    int offset = 0;

    Sample sresult = bs.Deserialize<Sample>("default", buffer, out offset); // last parameter if you need set and get offset

### BinaryAttribute

basic attribute for set type, and size for types how need this, you can use default type or you types overrided IBaseType, or class type which the included to data

**type** - required parameter

**TypeSize** - no required parameter, used for type BinaryString and other like him (need the size) for read and write required size

**TypeSizeName** - no required parameter, used for type BinaryString other and like him (need the size) for read property value for read and write required size

**ArraySize** - no required parameter, used for type BinaryList and other like him (need the array size) for read and write required element count

**ArraySizeName** - no required parameter, used for type BinaryString and other like him (need the array size) for read property value and read and write required element count

### BinarySchemeAttribute

must used for propertyes set scheme, on propertyes which included in scheme name

**BinarySchemeAttribute** can set one time for property, and sets serialize type, and optional size propertyes for types how need this

### BinaryPreCompileAttribute

must used for classes we needed precompiled

**BinaryPreCompileAttribute** can set many time for class, need for pre compile struct for many schemes, and can set pre init buffer size

**TypeStorage** have function PreCompileBinaryStructs, working with Assembly.GetExecutingAssembly() you can compille all structs in current assembly

### At now 

Working with Nullable struct types

### In the future 

Create serialization scenarios with functions

Create settings for control insertion append, size check, nullable check and other builder constructions
