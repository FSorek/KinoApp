namespace Kina.Mobile.Core.Model
{
    class ScoreRow
    {
        private bool[] isStarred;
        private string attributeName;

        public bool IsStarredOne
        {
            get { return isStarred[0]; }
        }
        public bool IsStarredTwo
        {
            get { return isStarred[1]; }
        }
        public bool IsStarredThree
        {
            get { return isStarred[2]; }
        }
        public bool IsStarredFour
        {
            get { return isStarred[3]; }
        }
        public bool IsStarredFive
        {
            get { return isStarred[4]; }
        }

        public string AttributeName
        {
            get { return attributeName; }
        }

        public ScoreRow(double averageScore, string attributeName)
        {
            this.attributeName = attributeName;
            isStarred = new bool[5];
            int averageInteger = (int)averageScore;
            for (int i = 0; i < averageInteger; i++)
            {
                isStarred[i] = true;
            }
        }
    }
}
