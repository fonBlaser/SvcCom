﻿namespace SvcCom.Schemas;

public class EnumValueSchema
{
    public string Name { get; }
    public int Value { get; }

    public EnumValueSchema(string name, int value)
    {
        if(string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Enum key(name) cannot be null or whitespace.", nameof(name));

        Name = name;
        Value = value;
    }

    //ToDo: implement different numeric value types for enum values
}