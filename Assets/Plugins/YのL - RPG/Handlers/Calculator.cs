namespace YNL.RPG
{
    public static class Calculator
    {
        public static int HP_Type1(int level, float bonus = 0)
        {
            int a = level / 10; // HPa(a) = 1000 + 1000 * (a * (a + 1) / 2); 
            int b = level % 10; // HPb(a, b) = (HPa(a + 1) - HPa(a)) / 10 * b;

            int baseHP = 100 * (5 * a * a + a * b + 5 * a + b + 10); // HP = HPa + HPb

            return baseHP + (int)(baseHP * bonus);
        }
    }
}