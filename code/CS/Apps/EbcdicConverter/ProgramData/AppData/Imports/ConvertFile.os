// this script performs an encoding conversion on a file
// parameters are 
// 1.  source file path and map i.e. $IMPORTS$\org-data.txt[Map1.B]
// 2.  target file path and map i.e. $IMPORTS$\org-data.ebc[Map1.A]

script ConvertFile // this is the name of the script
{  
  main
  {
    file src srcMap;
    file tgt tgtMap;
    exec convert f1 f2;
  }
}

