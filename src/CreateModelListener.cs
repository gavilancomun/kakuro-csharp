namespace kakuro {

public class CreateModelListener : KakuroBaseListener {

private GridController gc = new GridController();

public GridController getGridController() {
  return gc;
}

override
public void EnterHeader(KakuroParser.HeaderContext ctx) {
  gc.createRow();
}

override
public void EnterRow(KakuroParser.RowContext ctx) {
  gc.createRow();
}

override
public void EnterDown_across(KakuroParser.Down_acrossContext ctx) {
  int down = int.Parse(ctx.NUMBER(0).GetText());
  int across = int.Parse(ctx.NUMBER(1).GetText());
  gc.addDownAcross(down, across);
}

override
public void EnterAcross(KakuroParser.AcrossContext ctx) {
  int across = int.Parse(ctx.NUMBER().GetText());
  gc.addAcross(across);
}

override
public void EnterDown(KakuroParser.DownContext ctx) {
  int down = int.Parse(ctx.NUMBER().GetText());
  gc.addDown(down);
}

override
public void EnterEmpty(KakuroParser.EmptyContext ctx) {
  gc.addEmpty();
}

override
public void EnterValue(KakuroParser.ValueContext ctx) {
  gc.addValue(1);
}

}

}
