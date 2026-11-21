# Deploy NotesApi with an existing Dokploy MongoDB

Create a Docker Compose service using FaruqHAO/NotesApi, branch master,
Compose file compose.dokploy.yml, and repository root build path.
Use Compose mode, not Stack.

In Dokploy Environment set:
```dotenv
JWT_KEY=replace-with-output-of-openssl-rand-hex-32
MongoDBSettings__ConnectionString=mongodb://USER:PASSWORD@INTERNAL_HOST:27017/NotesDB?authSource=admin&directConnection=true
MongoDBSettings__DatabaseName=NotesDB
```

The database name in the URL must match DatabaseName; Mongo Identity uses the URL.
Use your existing database name in both places if migrating existing data.
The MongoDB service must be running and reachable on dokploy-network.
This file creates only the API; database persistence/backups are managed on the
existing MongoDB service. Never commit real credentials.

Add domain notes.baosat.dpdns.org, service api, port 8080, path /,
HTTPS off for the HTTP Cloudflare tunnel origin.
Cloudflare published hostname: notes.baosat.dpdns.org -> http://127.0.0.1:80.
Save and deploy.

Swagger: https://notes.baosat.dpdns.org/swagger/index.html
Register, log in, then Authorize using Bearer YOUR_TOKEN.
GET /health is liveness only, not a database connectivity check.
