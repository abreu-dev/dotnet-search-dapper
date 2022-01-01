®
aC:\Desenvolvimento\Repos\dotnet-search\src\DotNetSearch.API\Common\UnprocessableEntityResponse.cs
	namespace 	
DotNetSearch
 
. 
API 
. 
Common !
{ 
public 

class '
UnprocessableEntityResponse ,
{ 
public 
IEnumerable 
< 
string !
>! "
Errors# )
{* +
get, /
;/ 0
set1 4
;4 5
}6 7
} 
}		 Ü
YC:\Desenvolvimento\Repos\dotnet-search\src\DotNetSearch.API\Controllers\BaseController.cs
	namespace 	
DotNetSearch
 
. 
API 
. 
Controllers &
{ 
[ 
ApiController 
] 
[		 
Route		 

(		
 
$str		 
)		 
]		 
public

 

abstract

 
class

 
BaseController

 (
:

) *
ControllerBase

+ 9
{ 
	protected 
new 
IActionResult #
Response$ ,
(, -
ValidationResult- =
validationResult> N
)N O
{ 	
if 
( 
validationResult  
.  !
IsValid! (
)( )
{ 
return 
Ok 
( 
) 
; 
} 
else 
{ 
return 
UnprocessableEntity *
(* +
new+ .'
UnprocessableEntityResponse/ J
(J K
)K L
{ 
Errors 
= 
validationResult -
.- .
Errors. 4
.4 5
Select5 ;
(; <
x< =
=>> @
xA B
.B C
ErrorMessageC O
)O P
} 
) 
; 
} 
} 	
} 
} œ
^C:\Desenvolvimento\Repos\dotnet-search\src\DotNetSearch.API\Controllers\CategoriaController.cs
	namespace 	
DotNetSearch
 
. 
API 
. 
Controllers &
{		 
public

 

class

 
CategoriaController

 $
:

% &
BaseController

' 5
{ 
private 
readonly  
ICategoriaAppService - 
_categoriaAppService. B
;B C
public 
CategoriaController "
(" # 
ICategoriaAppService# 7
categoriaAppService8 K
)K L
{ 	 
_categoriaAppService  
=! "
categoriaAppService# 6
;6 7
} 	
[ 	
HttpGet	 
] 
public 
async 
Task 
< 
IEnumerable %
<% &
CategoriaContrato& 7
>7 8
>8 9
GetAll: @
(@ A
)A B
{ 	
return 
await  
_categoriaAppService -
.- .
GetAll. 4
(4 5
)5 6
;6 7
} 	
[ 	
HttpGet	 
( 
$str 
) 
] 
public 
async 
Task 
< 
CategoriaContrato +
>+ ,
GetById- 4
(4 5
Guid5 9
id: <
)< =
{ 	
return 
await  
_categoriaAppService -
.- .
GetById. 5
(5 6
id6 8
)8 9
;9 :
} 	
[ 	
HttpPost	 
] 
public   
async   
Task   
<   
IActionResult   '
>  ' (
Add  ) ,
(  , -
[  - .
FromBody  . 6
]  6 7
CategoriaContrato  8 I
categoriaContrato  J [
)  [ \
{!! 	
return"" 
Response"" 
("" 
await"" ! 
_categoriaAppService""" 6
.""6 7
Add""7 :
("": ;
categoriaContrato""; L
)""L M
)""M N
;""N O
}## 	
[%% 	
HttpPut%%	 
]%% 
public&& 
async&& 
Task&& 
<&& 
IActionResult&& '
>&&' (
Update&&) /
(&&/ 0
[&&0 1
FromBody&&1 9
]&&9 :
CategoriaContrato&&; L
categoriaContrato&&M ^
)&&^ _
{'' 	
return(( 
Response(( 
((( 
await(( ! 
_categoriaAppService((" 6
.((6 7
Update((7 =
(((= >
categoriaContrato((> O
)((O P
)((P Q
;((Q R
})) 	
[++ 	

HttpDelete++	 
(++ 
$str++ 
)++  
]++  !
public,, 
async,, 
Task,, 
<,, 
IActionResult,, '
>,,' (
Remove,,) /
(,,/ 0
Guid,,0 4
id,,5 7
),,7 8
{-- 	
return.. 
Response.. 
(.. 
await.. ! 
_categoriaAppService.." 6
...6 7
Remove..7 =
(..= >
id..> @
)..@ A
)..A B
;..B C
}// 	
}00 
}11 Ô

FC:\Desenvolvimento\Repos\dotnet-search\src\DotNetSearch.API\Program.cs
	namespace 	
DotNetSearch
 
. 
API 
{ 
public 

static 
class 
Program 
{ 
public 
static 
void 
Main 
(  
string  &
[& '
]' (
args) -
)- .
{		 	
CreateHostBuilder

 
(

 
args

 "
)

" #
.

# $
Build

$ )
(

) *
)

* +
.

+ ,
Run

, /
(

/ 0
)

0 1
;

1 2
} 	
public 
static 
IHostBuilder "
CreateHostBuilder# 4
(4 5
string5 ;
[; <
]< =
args> B
)B C
=>D F
Host 
.  
CreateDefaultBuilder %
(% &
args& *
)* +
. $
ConfigureWebHostDefaults )
() *

webBuilder* 4
=>5 7
{ 

webBuilder 
. 

UseStartup )
<) *
Startup* 1
>1 2
(2 3
)3 4
;4 5
} 
) 
; 
} 
} ø
FC:\Desenvolvimento\Repos\dotnet-search\src\DotNetSearch.API\Startup.cs
	namespace		 	
DotNetSearch		
 
.		 
API		 
{

 
public 

class 
Startup 
{ 
public 
IConfiguration 
Configuration +
{, -
get. 1
;1 2
}3 4
public 
Startup 
( 
IConfiguration %
configuration& 3
)3 4
{ 	
Configuration 
= 
configuration )
;) *
} 	
public 
void 
ConfigureServices %
(% &
IServiceCollection& 8
services9 A
)A B
{ 	
services 
. 
AddControllers #
(# $
)$ %
;% &
services 
. 
RegisterServices %
(% &
Configuration& 3
)3 4
;4 5
services 
. 
AddSwaggerGen "
(" #
c# $
=>% '
{ 
c 
. 

SwaggerDoc 
( 
$str !
,! "
new# &
OpenApiInfo' 2
{3 4
Title5 :
=; <
$str= O
,O P
VersionQ X
=Y Z
$str[ _
}` a
)a b
;b c
} 
) 
; 
} 	
public 
void 
	Configure 
( 
IApplicationBuilder 1
app2 5
,5 6
IWebHostEnvironment7 J
envK N
)N O
{ 	
if   
(   
env   
.   
IsDevelopment   !
(  ! "
)  " #
)  # $
{!! 
app"" 
."" %
UseDeveloperExceptionPage"" -
(""- .
)"". /
;""/ 0
app## 
.## 

UseSwagger## 
(## 
)##  
;##  !
app$$ 
.$$ 
UseSwaggerUI$$  
($$  !
c$$! "
=>$$# %
c$$& '
.$$' (
SwaggerEndpoint$$( 7
($$7 8
$str$$8 R
,$$R S
$str$$T i
)$$i j
)$$j k
;$$k l
}%% 
app'' 
.'' 
UseHttpsRedirection'' #
(''# $
)''$ %
;''% &
app)) 
.)) 

UseRouting)) 
()) 
))) 
;)) 
app++ 
.++ 
UseAuthorization++  
(++  !
)++! "
;++" #
app-- 
.-- 
UseEndpoints-- 
(-- 
	endpoints-- &
=>--' )
{.. 
	endpoints// 
.// 
MapControllers// (
(//( )
)//) *
;//* +
}00 
)00 
;00 
}11 	
}22 
}33 