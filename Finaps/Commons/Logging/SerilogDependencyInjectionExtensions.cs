﻿using System;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace Finaps.Commons.Logging
{
  public static class SerilogDependencyInjectionExtensions
  {
    public static Serilog.ILogger CreateSerilogLogger(this IConfiguration configuration)
    {
      var seqServerUrl = configuration["Serilog:SeqServerUrl"];
      var logstashUrl = configuration["Serilog:LogstashUrl"];
      return new LoggerConfiguration()
          .MinimumLevel.Verbose()
          // .Enrich.WithProperty("ApplicationContext", AppName)
          .Enrich.FromLogContext()
          .WriteTo.Console()
          .WriteTo.Seq(string.IsNullOrWhiteSpace(seqServerUrl) ? "http://seq" : seqServerUrl)
          .WriteTo.Http(string.IsNullOrWhiteSpace(logstashUrl) ? "http://logstash:8080" : logstashUrl)
          .ReadFrom.Configuration(configuration)
          .CreateLogger();
    }
  }
}
