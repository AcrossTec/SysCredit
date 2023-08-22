namespace SysCredit.Helpers.Delegates;

public delegate void Send<T>(T Value);

public delegate void Request<T>(T Value);

public delegate void Fetch<T>(T Value);
