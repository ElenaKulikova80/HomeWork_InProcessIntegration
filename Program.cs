using System.Diagnostics;

long sumIntUsual = 0; //обычное вычисление суммы 
long totalSumIntParallel = 0;//общая сумма параллельного вычисления суммы
long count = 100000;
long[] array = new long[count];

Random random = new Random();
Stopwatch stopWatch = new Stopwatch();

//формирование массива интов 
for (long i = 1; i < count; i++)
{
    array[i] = random.Next(1, (int)count);
}

//обычное вычисление суммы интов
stopWatch.Start();
sumIntUsual = IntSumma(array);
stopWatch.Stop();
Console.WriteLine("Сумма интов = {0}", count);
Console.WriteLine("Обычное вычисление суммы интов, Время выполнения = {0}, мс", stopWatch.ElapsedMilliseconds);

//параллельное вычисление суммы интов
stopWatch.Start();
Parallel.ForEach(array,
                index =>
                {
                    Interlocked.Add(ref totalSumIntParallel, array[index]);
                });
stopWatch.Stop();
Console.WriteLine("Параллельное вычисление суммы интов, Время выполнения = {0}, мс", stopWatch.ElapsedMilliseconds);

//для вычисление суммы интов с помощью PLINQ
stopWatch.Start();
SumAsParallel(array);
stopWatch.Stop();
Console.WriteLine("Параллельное (PLINQ) вычисление суммы интов, Время выполнения = {0}, мс", stopWatch.ElapsedMilliseconds);

//функция для последовательного вычисления суммы
static long IntSumma(long[] arr)
{
    long sumInt = 0;
    foreach (long x in arr)
    {
        sumInt += arr[x];
    }
    return sumInt;
}

//функция для параллельного вычисления суммы
static long SumAsParallel(long[] array)
{
    return array.AsParallel().Sum();
}

//var options = new ParallelOptions()
//{ MaxDegreeOfParallelism = Environment.ProcessorCount };