namespace PBallServices;

public partial interface IScrambleService
{
    string Descramble(string Text);
    string Scramble(string Text);
}
