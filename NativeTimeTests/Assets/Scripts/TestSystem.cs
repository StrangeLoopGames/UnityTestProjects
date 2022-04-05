using StrangeLoopGames.NativeTime;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using UnityEngine;

[BurstCompile]
public struct TestJob : IJob
{
    public NativeArray<long> result;
    float x;

    public void Execute()
    {
        var v = ValueStopwatch.StartNew();


        for (int i = 0; i < 100000; i++)
            x = Mathf.Sin(x + 1.0f);

        result[0] = v.ElapsedTicks.Ticks;
    }
}

public partial class TestSystem : SystemBase
{
    protected override void OnUpdate()
    {
        var v = ValueStopwatch.StartNew();
        Thread.Sleep(10);
        Debug.Log($"From OnUpdate: {v.ElapsedTicks}");

        using var result = new NativeArray<long>(1, Allocator.TempJob);
        var job = new TestJob();
        job.result = result;
        var handle = job.Schedule();
        handle.Complete();
        var time = TimeSpan.FromTicks(result[0]);

        Debug.Log($"From Job: {time}");
    }
}
