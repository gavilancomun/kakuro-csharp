namespace kakuro {
  using Antlr4.Runtime;
  using Antlr4.Runtime.Tree;
  using System;
  using System.IO;

public class Interpreter {

public static Optional<CreateModelListener> interpretRule(TextReader r, Func<KakuroParser, IParseTree> rule) {
  try {
    var input = new AntlrInputStream(r);
    var lexer = new KakuroLexer(input);
    var tokens = new CommonTokenStream(lexer);
    var parser = new KakuroParser(tokens);
    var tree = rule(parser);
    var walker = new ParseTreeWalker();
    var modelListener = new CreateModelListener();
    walker.Walk(modelListener, tree);
    return Optional<CreateModelListener>.ofNullable(modelListener);
  }
  catch (IOException ex) {
    return Optional<CreateModelListener>.empty();
  }
}

public static GridController interpret(TextReader r) {
  return interpretRule(r, parser => parser.grid())
    .Select(ml => ml.getGridController())
    .GetOr(new GridController());
}

}

}