using System.Reflection;
using System.Text;
using API_projet_parc_informatique.BD;
using API_projet_parc_informatique.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Ajouter le contexte de base de données SQLite
builder.Services.AddDbContext<ParcInfoBdContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Ajouter les contrôleurs
builder.Services.AddControllers();

// Authentification JWT
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});

// Swagger avec annotations XML + SwaggerOperation + sécurité JWT
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    // Documentation de l'API
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "API Parc Informatique",
        Version = "v1",
        Description = "API pour la gestion d'un parc informatique",
        Contact = new OpenApiContact
        {
            Name = "Votre Nom",
            Email = "votre.email@exemple.com"
        }
    });

    // Inclure les commentaires XML (nécessite <GenerateDocumentationFile>true</GenerateDocumentationFile> dans le .csproj)
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);

    // Activer les annotations [SwaggerOperation]
    c.EnableAnnotations();

    // Sécurité JWT
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme (Example: 'Bearer YOUR_TOKEN_HERE')",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

var app = builder.Build();

SQLitePCL.Batteries.Init();

// Pipeline de requêtes
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API Parc Informatique v1");
    });
}

app.UseHttpsRedirection();

app.UseAuthentication(); // JWT d'abord
app.UseAuthorization();

app.MapControllers();

app.Run();
