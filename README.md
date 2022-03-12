# simple-ini
A very simple INI class for C#.



Usage:

// initialize an ini variable
public INI ini = new INI("example.ini");

// load settings
ini.readSettings();

// reading a value
// the second parameter on all of these methods is optional, if the setting isn't found in the file, it will return the passed value (or default if not passed)

bool bValue = ini.readBool("bool_example", true);
long lValue = ini.readLong("long_example", 1337);
string sValue = ini.readString("string_example", "optional");
double dValue = ini.readDouble("double_example", 12.345);

// writing a value
// the second parameter takes an 'object' variable type

ini.writeItem("write_example", variable);
