# Dlt.Net
A C# library to handle AUTOSAR/GENIVI DLT protocol, which is based on AUTOSAR Specification of Diagnostic Log and Trace V1.2.0 R4.0 Rev3, Section 7.7 Protocol Specification.
Inspired by [PyDLT](https://github.com/mikiepure/pydlt)

### Limitation

- Currently, Dlt.Net just supports parsing Dlt messages. It can't create byte array yet.
 
- The following format of Type Info in a Payload has not been supported.
  - TYPE_LENGTH_128BIT
  - TYPE_ARRAY
  - VARIABLE_INFO
  - FIXED_POINT
  - TRACE_INFO
  - TYPE_STRUCT
