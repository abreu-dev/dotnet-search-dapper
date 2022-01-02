š
lC:\Desenvolvimento\Repos\dotnet-search\src\DotNetSearch.Application\AutoMapper\DotNetSearchMappingProfile.cs
	namespace 	
DotNetSearch
 
. 
Application "
." #

AutoMapper# -
{ 
public 

class &
DotNetSearchMappingProfile +
:, -
Profile. 5
{ 
public		 &
DotNetSearchMappingProfile		 )
(		) *
)		* +
{

 	
	CreateMap 
< 
	Categoria 
,  
CategoriaContrato! 2
>2 3
(3 4
)4 5
. 

ReverseMap 
( 
) 
; 
	CreateMap 
< 
	Categoria 
,  
	Categoria! *
>* +
(+ ,
), -
;- .
} 	
} 
} °
bC:\Desenvolvimento\Repos\dotnet-search\src\DotNetSearch.Application\Contratos\CategoriaContrato.cs
	namespace 	
DotNetSearch
 
. 
Application "
." #
	Contratos# ,
{ 
public 

class 
CategoriaContrato "
:# $
Contrato% -
{ 
public 
Guid 
Id 
{ 
get 
; 
set !
;! "
}# $
public 
string 
Nome 
{ 
get  
;  !
set" %
;% &
}' (
}		 
}

 ã
YC:\Desenvolvimento\Repos\dotnet-search\src\DotNetSearch.Application\Contratos\Contrato.cs
	namespace 	
DotNetSearch
 
. 
Application "
." #
	Contratos# ,
{ 
public 

abstract 
class 
Contrato "
{# $
}% &
} Ç
]C:\Desenvolvimento\Repos\dotnet-search\src\DotNetSearch.Application\Interfaces\IAppService.cs
	namespace 	
DotNetSearch
 
. 
Application "
." #

Interfaces# -
{ 
public 

	interface 
IAppService  
<  !
	TContrato! *
>* +
where		 
	TContrato		 
:		 
Contrato		 "
{

 
Task 
< 
IEnumerable 
< 
	TContrato "
>" #
># $
GetAll% +
(+ ,
), -
;- .
Task 
< 
	TContrato 
> 
GetById 
(  
Guid  $
id% '
)' (
;( )
} 
} ˜
fC:\Desenvolvimento\Repos\dotnet-search\src\DotNetSearch.Application\Interfaces\ICategoriaAppService.cs
	namespace 	
DotNetSearch
 
. 
Application "
." #

Interfaces# -
{ 
public 

	interface  
ICategoriaAppService )
:* +
IAppService, 7
<7 8
CategoriaContrato8 I
>I J
{		 
Task

 
<

 
ValidationResult

 
>

 
Add

 "
(

" #
CategoriaContrato

# 4
categoriaContrato

5 F
)

F G
;

G H
Task 
< 
ValidationResult 
> 
Update %
(% &
CategoriaContrato& 7
categoriaContrato8 I
)I J
;J K
Task 
< 
ValidationResult 
> 
Remove %
(% &
Guid& *
id+ -
)- .
;. /
} 
} £
ZC:\Desenvolvimento\Repos\dotnet-search\src\DotNetSearch.Application\Services\AppService.cs
	namespace

 	
DotNetSearch


 
.

 
Application

 "
.

" #
Services

# +
{ 
public 

abstract 
class 

AppService $
<$ %
	TContrato% .
,. /
TEntity0 7
>7 8
:9 :
IAppService; F
<F G
	TContratoG P
>P Q
where 
	TContrato 
: 
Contrato "
where 
TEntity 
: 
Entity 
{ 
private 
readonly 
IMapper  
_mapper! (
;( )
private 
readonly 
IRepository $
<$ %
TEntity% ,
>, -
_repository. 9
;9 :
	protected 

AppService 
( 
IMapper $
mapper% +
,+ ,
IRepository (
<( )
TEntity) 0
>0 1

repository2 <
)< =
{ 	
_mapper 
= 
mapper 
; 
_repository 
= 

repository $
;$ %
} 	
public 
async 
Task 
< 
IEnumerable %
<% &
	TContrato& /
>/ 0
>0 1
GetAll2 8
(8 9
)9 :
{ 	
return 
_mapper 
. 
Map 
< 
IEnumerable *
<* +
	TContrato+ 4
>4 5
>5 6
(6 7
await7 <
_repository= H
.H I
GetAllI O
(O P
)P Q
)Q R
;R S
} 	
public 
async 
Task 
< 
	TContrato #
># $
GetById% ,
(, -
Guid- 1
id2 4
)4 5
{   	
return!! 
_mapper!! 
.!! 
Map!! 
<!! 
	TContrato!! (
>!!( )
(!!) *
await!!* /
_repository!!0 ;
.!!; <
GetById!!< C
(!!C D
id!!D F
)!!F G
)!!G H
;!!H I
}"" 	
}## 
}$$ ø3
cC:\Desenvolvimento\Repos\dotnet-search\src\DotNetSearch.Application\Services\CategoriaAppService.cs
	namespace 	
DotNetSearch
 
. 
Application "
." #
Services# +
{ 
public 

class 
CategoriaAppService $
:% &

AppService' 1
<1 2
CategoriaContrato2 C
,C D
	CategoriaE N
>N O
,O P 
ICategoriaAppService 
{ 
private 
readonly 
IMapper  
_mapper! (
;( )
private 
readonly  
ICategoriaRepository - 
_categoriaRepository. B
;B C
public 
CategoriaAppService "
(" #
IMapper# *
mapper+ 1
,1 2 
ICategoriaRepository# 7
categoriaRepository8 K
)K L
: 
base 
( 
mapper 
, 
categoriaRepository .
). /
{ 	
_mapper 
= 
mapper 
;  
_categoriaRepository  
=! "
categoriaRepository# 6
;6 7
} 	
public 
async 
Task 
< 
ValidationResult *
>* +
Add, /
(/ 0
CategoriaContrato0 A
categoriaContratoB S
)S T
{ 	
var 
	categoria 
= 
_mapper #
.# $
Map$ '
<' (
	Categoria( 1
>1 2
(2 3
categoriaContrato3 D
)D E
;E F
var   
validationResult    
=  ! "
new  # &!
AddCategoriaValidator  ' <
(  < =
)  = >
.  > ?
Validate  ? G
(  G H
	categoria  H Q
)  Q R
;  R S
if!! 
(!! 
validationResult!!  
.!!  !
IsValid!!! (
)!!( )
{""  
_categoriaRepository## $
.##$ %
Add##% (
(##( )
	categoria##) 2
)##2 3
;##3 4
await$$  
_categoriaRepository$$ *
.$$* +

UnitOfWork$$+ 5
.$$5 6
Commit$$6 <
($$< =
)$$= >
;$$> ?
}%% 
return'' 
validationResult'' #
;''# $
}(( 	
public** 
async** 
Task** 
<** 
ValidationResult** *
>*** +
Update**, 2
(**2 3
CategoriaContrato**3 D
categoriaContrato**E V
)**V W
{++ 	
var,, 
	categoria,, 
=,, 
_mapper,, #
.,,# $
Map,,$ '
<,,' (
	Categoria,,( 1
>,,1 2
(,,2 3
categoriaContrato,,3 D
),,D E
;,,E F
var.. 
validationResult..  
=..! "
new..# &$
UpdateCategoriaValidator..' ?
(..? @
)..@ A
...A B
Validate..B J
(..J K
	categoria..K T
)..T U
;..U V
if// 
(// 
validationResult//  
.//  !
IsValid//! (
)//( )
{00 
var11 
dbEntity11 
=11 
await11 $ 
_categoriaRepository11% 9
.119 :
GetById11: A
(11A B
	categoria11B K
.11K L
Id11L N
)11N O
;11O P
if22 
(22 
dbEntity22 
==22 
null22  $
)22$ %
{33 
validationResult44 $
.44$ %
Errors44% +
.44+ ,
Add44, /
(44/ 0
new440 3
ValidationFailure444 E
(44E F
$str44F H
,44H I
DomainMessages55 &
.55& '
NotFound55' /
.55/ 0
Format550 6
(556 7
$str557 B
)55B C
.55C D
Message55D K
)55K L
)55L M
;55M N
}66 
else77 
{88 
_mapper99 
.99 
Map99 
(99  
	categoria99  )
,99) *
dbEntity99+ 3
)993 4
;994 5 
_categoriaRepository:: (
.::( )
Update::) /
(::/ 0
dbEntity::0 8
)::8 9
;::9 :
await;;  
_categoriaRepository;; .
.;;. /

UnitOfWork;;/ 9
.;;9 :
Commit;;: @
(;;@ A
);;A B
;;;B C
}<< 
}== 
return?? 
validationResult?? #
;??# $
}@@ 	
publicBB 
asyncBB 
TaskBB 
<BB 
ValidationResultBB *
>BB* +
RemoveBB, 2
(BB2 3
GuidBB3 7
idBB8 :
)BB: ;
{CC 	
varDD 
validationResultDD  
=DD! "
newDD# &$
RemoveCategoriaValidatorDD' ?
(DD? @
)DD@ A
.DDA B
ValidateDDB J
(DDJ K
idDDK M
)DDM N
;DDN O
ifEE 
(EE 
validationResultEE  
.EE  !
IsValidEE! (
)EE( )
{FF 
varGG 
dbEntityGG 
=GG 
awaitGG $ 
_categoriaRepositoryGG% 9
.GG9 :
GetByIdGG: A
(GGA B
idGGB D
)GGD E
;GGE F
ifHH 
(HH 
dbEntityHH 
==HH 
nullHH  $
)HH$ %
{II 
validationResultJJ $
.JJ$ %
ErrorsJJ% +
.JJ+ ,
AddJJ, /
(JJ/ 0
newJJ0 3
ValidationFailureJJ4 E
(JJE F
$strJJF H
,JJH I
DomainMessagesKK &
.KK& '
NotFoundKK' /
.KK/ 0
FormatKK0 6
(KK6 7
$strKK7 B
)KKB C
.KKC D
MessageKKD K
)KKK L
)KKL M
;KKM N
}LL 
elseMM 
{NN  
_categoriaRepositoryOO (
.OO( )
RemoveOO) /
(OO/ 0
dbEntityOO0 8
)OO8 9
;OO9 :
awaitPP  
_categoriaRepositoryPP .
.PP. /

UnitOfWorkPP/ 9
.PP9 :
CommitPP: @
(PP@ A
)PPA B
;PPB C
}QQ 
}RR 
returnTT 
validationResultTT #
;TT# $
}UU 	
}VV 
}WW 