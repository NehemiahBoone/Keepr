cd wwwsrc
npm run build
cd ..
dotnet publish -c Release
docker build -t keeprboone ./
docker tag keeprboone registry.heroku.com/keeprboone/web
docker push registry.heroku.com/keeprboone/web
heroku container:release web -a keeprboone
echo press any key
read end