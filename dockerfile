FROM mcr.microsoft.com/dotnet/core/sdk:3.0 

WORKDIR /app




# if the csproj files have not changed since the last build
# on this laptop, then, the above layers will be recovered from cache,
# and we don't need to run restore again.

# we use .dockerignore to hide files from being copied with
# the build context, so COPY here won't see them either.
# which files? bin/, obj/, etc.
COPY . ./

#WORKDIR /app/YourStoreWeb

RUN dotnet publish YourStoreWeb -c Release -o publish 


WORKDIR /app/publish


ENV ASPNETCORE_URLS http://+:80

CMD [ "dotnet", "YourStoreWeb.dll" ]
# this final image does not have the SDK, nor the source code,
# only 1. what's in the base image, #2 my published build output.
