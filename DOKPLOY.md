# Deploy NotesApi

Create a Docker Compose service in Dokploy, select FaruqHAO/NotesApi,
branch `master`, repository root build path and `compose.dokploy.yml`.
Use Compose mode, not Stack. MongoDB is included and has no public port.

Generate a secret on Ubuntu with `openssl rand -hex 32`. Save in Dokploy Environment:

```dotenv
JWT_KEY=replace-with-the-generated-key
```

Add a Dokploy domain: `notes.baosat.dpdns.org`, service `api`, port `8080`,
path `/`, HTTPS disabled for the HTTP tunnel origin. Deploy.
In the existing Cloudflare Tunnel add hostname `notes.baosat.dpdns.org`
with HTTP service `127.0.0.1:80` (cloudflared runs on the Ubuntu host).

Open `https://notes.baosat.dpdns.org/swagger/index.html`.
Register using POST /api/Auth/register, log in with POST /api/Auth/login,
then click Authorize and enter `Bearer YOUR_TOKEN` to call /api/Notes.
`GET /health` is an unauthenticated liveness check, not a database readiness check.

The named mongo-data volume persists through redeployment. Back it up separately.
Restart policies restart containers after Docker starts; enable VM start-at-boot in
Proxmox and Docker/cloudflared services on Ubuntu. Reboots still cause downtime.

Both notes controllers require JWT authentication and use the authenticated user ID.
Changing JWT_KEY invalidates existing tokens. No PostgreSQL or Redis is required.
