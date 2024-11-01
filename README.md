# Dlt.Net
A C# library to handle AUTOSAR/GENIVI DLT.

### Limitation

- Currently, Dlt.Net just supports parsing Dlt messages. It can't create byte array yet.
 
- The following format of Type Info in a Payload has not been supported.
  - TYPE_LENGTH_128BIT
  - TYPE_ARRAY
  - VARIABLE_INFO
  - FIXED_POINT
  - TRACE_INFO
  - TYPE_STRUCT
