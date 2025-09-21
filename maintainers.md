# Publishing a new version:

```
> dotnet pack --configuration Release --output ./nupkgs
> dotnet nuget push Unidecode.NET.2.2.1.nupkg --api-key <API_KEY> --source https://api.nuget.org/v3/index.json
```
