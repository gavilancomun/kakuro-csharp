namespace kakuro {

  using System;
  using System.IO;

  public class TestParse {

    public void testAPI() {
      var grid = new GridController();
      grid.createRow().addEmpty().addDown(4).addDown(22).addEmpty().addDown(16).addDown(3);
      grid.createRow().addAcross(3).addValue(2).addDownAcross(16, 6).addValue(2);
      grid.createRow().addAcross(18).addValue(5);
      grid.createRow().addEmpty().addDownAcross(17, 23).addValue(3).addDown(14);
      grid.createRow().addAcross(9).addValue(2).addAcross(6).addValue(2);
      grid.createRow().addAcross(15).addValue(2).addAcross(12).addValue(2);
      grid.solve();
    }

    public void testParse() {
      var k = "XXXXX  4\\-   22\\-  XXXXX  16\\-  3\\-\n" +
                 "-\\3   .      .      16\\6  .      .\n" +
                 "-\\18  .      .      .      .      .\n" +
                 "XXXXX  17\\23 .      .      .      14\\-\n" +
                 "-\\ 9  .      .      -\\6   .      .\n" +
                 "-\\15  .      .      -\\12  .      .\n";
      GridController gc = Interpreter.interpret(new StringReader(k));
      gc.solve();
    }

  }

}