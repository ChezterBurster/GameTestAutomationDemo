using System.Text;

public static class HtmlReportExporter
{
    public static void Export(ReportSession session)
    {
        var logsBuilder = new StringBuilder();

        foreach (var log in session.Logs)
        {
            var screenshotHtml = BuildScreenshotHtml(session, log);

            var exceptionHtml = BuildExceptionHtml(log);

            logsBuilder.Append($"""
<tr>
    <td>{log.Timestamp:HH:mm:ss}</td>

    <td class="{log.Level.ToString().ToUpper()}">
        {log.Level.ToString().ToUpper()}
    </td>

    <td>
        {log.Message}

        {screenshotHtml}

        {exceptionHtml}
    </td>
</tr>

""");
        }

        var endTime = session.EndTime ?? DateTime.Now;

        var duration = endTime - session.StartTime;

        var status = session.Success ? "PASS" : "FAIL";

        var statusClass = session.Success
            ? "status-pass"
            : "status-fail";

        var finalHtml = $$"""
<!DOCTYPE html>
<html lang="en">

<head>
<meta charset="UTF-8">

<title>{{session.TestName}}</title>

<style>

body
{background-color: #1e1e1e;
    color: #110b0b;
    font-family: Arial, Helvetica, sans-serif;
    margin: 0;
    padding: 20px;
}

h1
{margin-bottom: 10px;
}

.summary
{background-color: #2a2a2a;
    padding: 20px;
    border-radius: 10px;
    margin-bottom: 20px;
}

.status-pass
{color: #4caf50;
    font-weight: bold;
}

.status-fail
{color: #f44336;
    font-weight: bold;
}

table
{width: 100%;
    border-collapse: collapse;
    background-color: #2a2a2a;
    border-radius: 10px;
    overflow: hidden;
}

th
{background-color: #333;
    text-align: left;
    padding: 12px;
}

td
{padding: 10px;
    border-top: 1px solid #444;
    vertical-align: top;
}

.INFO {color: #64b5f6; }
.STEP {color: #ffd54f; }
.WARNING {color: #ffb74d; }
.ERROR {color: #ef5350; }
.PASSED {color: #81c784; }
.DEBUG {color: #ba68c8; }

.screenshot
{margin-top: 10px;
    max-width: 500px;
    border-radius: 8px;
    border: 1px solid #555;
}

.exception
{margin-top: 10px;
    padding: 10px;
    background-color: #3a1f1f;
    border-left: 4px solid #ef5350;
    white-space: pre-wrap;
    font-family: Consolas, monospace;
}

</style>
</head>

<body>

<h1>{{session.TestName}}</h1>

<div class="summary">

    <p>
        <strong>Status:</strong>
        <span class="{{statusClass}}">
            {{status}}
        </span>
    </p>

    <p>
        <strong>Started:</strong>
        {{session.StartTime}}
    </p>

    <p>
        <strong>Ended:</strong>
        {{endTime}}
    </p>

    <p>
        <strong>Duration:</strong>
        {{duration.TotalSeconds:F2}}s
    </p>

    <p>
        <strong>Total Logs:</strong>
        {{session.Logs.Count}}
    </p>

</div>

<table>

<thead>
<tr>
    <th>Time</th>
    <th>Level</th>
    <th>Message</th>
</tr>
</thead>

<tbody>

{{logsBuilder}}

</tbody>

</table>

</body>
</html>
""";

        var outputPath = Path.Combine(
            session.TestPath,
            "report.html");

        File.WriteAllText(outputPath, finalHtml);
    }

    private static string BuildScreenshotHtml(
        ReportSession session,
        LogEntry log)
    {
        if (string.IsNullOrWhiteSpace(log.ScreenshotPath))
            return string.Empty;

        var relativePath = Path.GetRelativePath(
            session.TestPath,
            log.ScreenshotPath).Replace("\\", "/");

        return $"""
<br>

<img class="screenshot"
     src="{relativePath}"
     alt="screenshot">
""";
    }

    private static string BuildExceptionHtml(LogEntry log)
    {
        if (string.IsNullOrWhiteSpace(log.Exception))
            return string.Empty;

        return $"""
<div class="exception">
{log.Exception}
</div>
""";
    }
}
