namespace MainCharacterGenderPatcher
{
    public class Dialogue
    {
        public int sceneIndex { get; set; }
        public int dialogueIndex { get; set; }
        public Dialogue(int sceneIndex, int dialogueIndex)
        {
            this.sceneIndex = sceneIndex;
            this.dialogueIndex = dialogueIndex;
        }
    }
}
