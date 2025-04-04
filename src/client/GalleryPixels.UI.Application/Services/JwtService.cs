﻿using System.Globalization;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using GalleryPixels.UI.Domain.Services;

namespace GalleryPixels.UI.Application.Services;

public class JwtService : IJwtService
{
    public ClaimsPrincipal Deserialize(string jwtToken)
    {
        var segments = jwtToken.Split('.');

        if (segments.Length != 3)
        {
            throw new Exception("Invalid JWT");
        }

        var dataSegment = Encoding.UTF8.GetString(FromUrlBase64(segments[1]));
        var data = JsonSerializer.Deserialize<JsonObject>(dataSegment);

        var claims = new Claim[data!.Count];
        var index = 0;
        foreach (var entry in data)
        {
            claims[index] = JwtNodeToClaim(entry.Key, entry.Value!);
            index++;
        }

        var claimIdentity = new ClaimsIdentity(claims, "jwt");
        var principal = new ClaimsPrincipal([claimIdentity]);

        return principal;
    }

    private static Claim JwtNodeToClaim(string key, JsonNode node)
    {
        var jsonValue = node.AsValue();

        if (jsonValue.TryGetValue<string>(out var str))
        {
            return new Claim(key, str, ClaimValueTypes.String);
        }
        else if (jsonValue.TryGetValue<double>(out var num))
        {
            return new Claim(key, num.ToString(CultureInfo.InvariantCulture), ClaimValueTypes.Double);
        }
        else
        {
            throw new Exception("Unsupported JWT claim type");
        }
    }

    private static byte[] FromUrlBase64(string jwtSegment)
    {
        var fixedBase64 = jwtSegment.Replace('-', '+').Replace('_', '/');
        switch (fixedBase64.Length % 4)
        {
            case 2: fixedBase64 += "=="; break;
            case 3: fixedBase64 += "="; break;
        }

        return Convert.FromBase64String(fixedBase64);
    }
}