# FormulaTester
Console application used for quickly testing multiple implementations, comparing accuracy as well as speed over many runs. Tests can be performed with randomly generated input to get a wide spread of states. Currently set up for testing formula forms, but can easily be expanded to test different classes that might perform different steps to achieve the same end result.

Currently the tester works by adding the formula in one of the existing formula classes. The classes take a variable number of double parameters that can be used however one wishes. For running tests, the number of iterations and random number cases can be set in source code, although in the future I might make it an option that can be set through the console application.

Add formula classes using the existing ones as a template. For changing the number of formula classes considered, there are just a couple of lines of code to alter in the core program to add a given formula class to the suite list to be run.
