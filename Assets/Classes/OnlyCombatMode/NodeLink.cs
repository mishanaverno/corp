
namespace Map {
    public class NodeLink
    {
        public float w;
        private Node[] linkedNodes;

        public NodeLink(Node node1, Node node2, float w)
        {
            this.w = w;
            this.linkedNodes = new Node[2];
            this.linkedNodes[0] = node1;
            this.linkedNodes[1] = node2;
        }
    }
}
