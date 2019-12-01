# GeoLocApi
Web Api for Geo Localization

WebApi start on: http://localhost:60260
Scheme: ://{ServiceHost}:{ServicePort}/api/GeoLocations

JSON InputModel scheme:
{
  "ip":"134.201.250.155"
}

JSON Response scheme:
{
  "id": "...",
  "ip": "...",
  "city": "...",
  "country_name": "...",
  "continent_name": "..."
}
