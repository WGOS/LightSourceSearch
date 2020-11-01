## Light Source Search
A university project on Raspberry Pi.  
Basically it getting info from camera, scans for light source and points laser on it

## Environment variables
| Variable        | Value type | Description                              |
|-----------------|------------|------------------------------------------|
| LSS_PIN_SPEAKER | int        | GPIO Pin for speaker                     |
| LSS_PIN_LASER   | int        | GPIO Pin for laser pointer               |
| LSS_LOG_FILE    | bool       | Enable logging to log.txt                |
| LSS_DEBUG       | bool       | Enable debug and self-test functionality |
| LSS_SILENT      | bool       | Disable speaker                          |