dotnet publish -c Release
docker build -t keeprboone ./bin/Release/netcoreapp3.1/publish
docker tag keeprboone registry.heroku.com/keeprboone/web
docker push registry.heroku.com/keeprboone/web
heroku container:release web -a keeprboone