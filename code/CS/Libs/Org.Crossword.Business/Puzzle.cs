using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using Org.GS;

namespace Org.Crossword.Business
{
  public class Puzzle
  {
    public int Width { get; set; }
    public int Height { get; set; }
    public Dictionary<int, Word> Words { get; set; }
    public Dictionary<int, Clue> Clues { get; set; }
    public List<Cell> Cells { get; set; }
    private Dictionary<int, Dictionary<int, Cell>> _cells;

    public Puzzle(string fullPath)
    {
      try
      {
        string xml = File.ReadAllText(fullPath);
        var xmlElement = XElement.Parse(xml);
        PopulatePuzzle(xmlElement);
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to instantiate the Puzzle object.", ex); 
      }
    }

    public Cell GetCell(int x, int y)
    {
      if (!_cells.ContainsKey(x))
        return null;
      if (!_cells[x].ContainsKey(y))
        return null; 
      return _cells[x][y];
    }

    private void PopulatePuzzle(XElement xml)
    {
      this.Words = new Dictionary<int, Word>();
      this.Clues = new Dictionary<int, Clue>();
      this.Cells = new List<Cell>();
      _cells = new Dictionary<int, Dictionary<int, Cell>>();

      var rp = xml.Elements().Where(e => e.Name.LocalName == "rectangular-puzzle").FirstOrDefault();
      var cw = rp.Elements().Where(e => e.Name.LocalName == "crossword").FirstOrDefault();
      var grid = cw.Elements().Where(e => e.Name.LocalName == "grid").FirstOrDefault();

      this.Width = grid.Attribute("width").Value.ToInt32();
      this.Height = grid.Attribute("height").Value.ToInt32();

      var cellElements = grid.Elements().Where(e => e.Name.LocalName == "cell");

      foreach (var cellElement in cellElements)
      {
        int x = cellElement.Attribute("x").Value.ToInt32();
        int y = cellElement.Attribute("y").Value.ToInt32();
        string solution = cellElement.Attribute("solution") != null ? cellElement.Attribute("solution").Value : String.Empty;
        int? number = cellElement.Attribute("number") != null ? cellElement.Attribute("number").Value.ToInt32() : (int?) null;

        var cell = new Cell(x, y, solution, number);
        this.Cells.Add(cell);

        if (!_cells.ContainsKey(x))
          _cells.Add(x, new Dictionary<int, Cell>());

        _cells[x].Add(y, cell);
      }

      var wordElements = cw.Elements().Where(e => e.Name.LocalName == "word");

      foreach (var wordElement in wordElements)
      {
        int wordId = wordElement.Attribute("id").Value.ToInt32();
        string xRange = wordElement.Attribute("x").Value;
        string yRange = wordElement.Attribute("y").Value;
        var direction = xRange.Contains("-") ? Direction.Down : Direction.Across;

        var word = new Word(wordId, direction, xRange, yRange);
        this.Words.Add(word.WordId, word); 
      }

      var clueSets = cw.Elements().Where(e => e.Name.LocalName == "clues");

      foreach (var clueSet in clueSets)
      {
        var title = clueSet.Elements().Where(e => e.Name.LocalName == "title").FirstOrDefault();
        var b = title.Elements().Where(e => e.Name.LocalName == "b").FirstOrDefault();
        var direction = b.Value.ToLower() == "down" ? Direction.Down : Direction.Across;

        var clueElements = clueSet.Elements().Where(e => e.Name.LocalName == "clue"); 

        foreach (var clueElement in clueElements)
        {
          int wordId = clueElement.Attribute("word").Value.ToInt32();
          int number = clueElement.Attribute("number").Value.ToInt32();
          int format = clueElement.Attribute("format").Value.ToInt32();
          string phrase = clueElement.Value;
          var clue = new Clue(wordId, number, format, phrase, direction);
          this.Clues.Add(clue.WordId, clue); 
        }
      }
    }
  }
}
