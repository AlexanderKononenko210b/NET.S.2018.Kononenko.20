# NET.S.2018.Kononenko.20
Solving task day 20

1. In a text file, information about URL-addresses represented in the form
<sheme>://<host>/<URL-path>?<parameteres>
where the parameters segment is a set of pairs of the form key = value, with the URL-path and parameters segments or the parameters segment missing. Develop a type system (guided by the principles of SOLID) for exporting data obtained by parsing the information of a text file into an XML document
  
For those URLs that do not coincide with this pattern, "log" information by marking the specified lines as raw. Demonstrate the work on the example of the console application.
What changes need to be made to the type system, if the information in the source text file changes to another, hierarchically representable information.
