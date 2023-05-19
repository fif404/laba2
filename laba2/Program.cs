// Скрипников Сергей 22-ИСП-2.1
class BitString
{
    private ulong first;
    private ulong second;
    public BitString()
    {
        first = second = 0;
    }
    public BitString(ulong val)
    {
        first = val;
        second = 0;
    }
    public BitString(ulong val1, ulong val2)
    {
        first = val1;
        second = val2;
    }
    public void SetBit(int pos, bool val)
    {
        if (pos < 0 || pos > 63)
            throw new ArgumentException("Invalid bit position");
        if (pos < 32)
        {
            if (val)
                first |= 1ul << pos;
            else
                first &= ~(1ul << pos);
        }
        else
        {
            if (val)
                second |= 1ul << (pos - 32);
            else
                second &= ~(1ul << (pos - 32));
        }
    }
    public bool GetBit(int pos)
    {
        if (pos < 0 || pos > 63)
            throw new ArgumentException("Invalid bit position");

        return (pos < 32) ? ((first & (1ul << pos)) != 0) : ((second & (1ul << (pos - 32))) != 0);
    }
    public static BitString operator &(BitString b1, BitString b2)
    {
        return new BitString(b1.first & b2.first, b1.second & b2.second);
    }
    public static BitString operator |(BitString b1, BitString b2)
    {
        return new BitString(b1.first | b2.first, b1.second | b2.second);
    }
    public static BitString operator ^(BitString b1, BitString b2)
    {
        return new BitString(b1.first ^ b2.first, b1.second ^ b2.second);
    }
    public static BitString operator ~(BitString b)
    {
        return new BitString(~b.first, ~b.second);
    }
    public static BitString operator <<(BitString b, int cnt)
    {
        if (cnt < 0 || cnt > 63)
            throw new ArgumentException("Invalid shift value");
        if (cnt == 0)
            return b;
        if (cnt < 32)
            return new BitString(b.first << cnt | b.second >> (32 - cnt), b.second << cnt);
        return new BitString(0, b.first << (cnt - 32));
    }
    public static BitString operator >>(BitString b, int cnt)
    {
        if (cnt < 0 || cnt > 63)
            throw new ArgumentException("Invalid shift value");
        if (cnt == 0)
            return b;

        if (cnt < 32)
            return new BitString(b.first >> cnt, b.first << (32 - cnt) | b.second >> cnt);
        return new BitString(b.first >> (cnt - 32), 0);
    }
    public override string ToString()
    {
        return string.Format("{0:X16}{1:X16}", second, first);
    }
}
class Program
{
    static void Main(string[] args)
    {
        BitString b1 = new BitString(0x12345678ABCDEF00ul, 0x8877665544332211ul);
        BitString b2 = new BitString(0xF0E1D2C398765432ul, 0x1122334455667788ul);
        Console.WriteLine("b1             = {0}", b1.ToString());
        Console.WriteLine("b2             = {0}", b2.ToString());
        Console.WriteLine("b1 & b2        = {0}", (b1 & b2).ToString());
        Console.WriteLine("b1 | b2        = {0}", (b1 | b2).ToString());
        Console.WriteLine("b1 ^ b2        = {0}", (b1 ^ b2).ToString());
        Console.WriteLine("~b1            = {0}", (~b1).ToString());
        Console.WriteLine("b1 << 8        = {0}", (b1 << 8).ToString());
        Console.WriteLine("b1 >> 8        = {0}", (b1 >> 8).ToString());
        Console.ReadKey();
    }
}