public class RandomPathAgent : PathAgent {
    public override Pathfinder createPathfinder () {
        return new RandomPathfinder ();
    }
}