﻿namespace demoSqlite.RSA
{
    public interface IRSAHelper
    {
        string Encrypt(string text);
        string Decrypt(string encrypted);
    }
}
