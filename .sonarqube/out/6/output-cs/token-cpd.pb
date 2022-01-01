‘
lC:\Desenvolvimento\Repos\dotnet-search\src\DotNetSearch.Infra.CrossCutting.IoC\NativeInjectorBootStrapper.cs
	namespace 	
DotNetSearch
 
. 
Infra 
. 
CrossCutting )
.) *
IoC* -
{ 
public 

static 
class &
NativeInjectorBootStrapper 2
{ 
public 
static 
void 
RegisterServices +
(+ ,
this, 0
IServiceCollection1 C
servicesD L
,L M
IConfiguration, :
configuration; H
)H I
{ 	
services 
. 
AddDbContext !
<! "!
DotNetSearchDbContext" 7
>7 8
(8 9
options9 @
=>A C
options 
. 
UseSqlServer (
(( )
configuration) 6
.6 7
GetConnectionString7 J
(J K
$strK ^
)^ _
)_ `
)` a
;a b
services 
. 
	AddScoped 
< !
DotNetSearchDbContext 4
>4 5
(5 6
)6 7
;7 8
services 
. 
	AddScoped 
<  
ICategoriaRepository 3
,3 4
CategoriaRepository5 H
>H I
(I J
)J K
;K L
services 
. 
AddAutoMapper "
(" #
typeof# )
() *&
DotNetSearchMappingProfile* D
)D E
)E F
;F G
services 
. 
	AddScoped 
<  
ICategoriaAppService 3
,3 4
CategoriaAppService5 H
>H I
(I J
)J K
;K L
} 	
}   
}!! 