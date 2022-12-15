﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using System;

namespace Raspite.WinUI;

public sealed partial class App : Application
{
    public new static App Current => (App) Application.Current;

    public IServiceProvider? Services { get; private set; }

    public Window? Window { get; private set; }

    public App()
    {
        InitializeComponent();
    }

    // This method gets executed before the constructor, so we have to initialize the services and the window inside it.
    protected override void OnLaunched(LaunchActivatedEventArgs eventArgs)
    {
        Services = new ServiceCollection()
            .AddTransient<ShellViewModel>()
            .AddTransient<MenuViewModel>()
            .AddTransient<DialogService>()
            .AddTransient<NbtSerializerService>()
            .BuildServiceProvider();

        Window = new ShellView()
        {
            ExtendsContentIntoTitleBar = true
        };

        Window?.Activate();
    }
}