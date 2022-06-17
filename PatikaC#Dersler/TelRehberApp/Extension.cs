namespace TelRehberApp{

    public static class Extension{
//int degerin basamak sayisini bulan bir extension method yazmis olduk
        public static int FindDigitNumber(this string number){
            return number.Length;
        }
    }
}