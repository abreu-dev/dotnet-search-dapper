Ì
VC:\Desenvolvimento\Repos\dotnet-search\src\DotNetSearch.Domain\Common\DomainMessage.cs
	namespace 	
DotNetSearch
 
. 
Domain 
. 
Common $
{ 
public 

class 
DomainMessage 
{ 
public 
string 
Message 
{ 
get  #
;# $
}% &
public 
DomainMessage 
( 
string #
message$ +
)+ ,
{ 	
Message		 
=		 
message		 
;		 
}

 	
public 
DomainMessage 
Format #
(# $
params$ *
object+ 1
[1 2
]2 3
args4 8
)8 9
{ 	
return 
new 
DomainMessage $
($ %
string% +
.+ ,
Format, 2
(2 3
Message3 :
,: ;
args< @
)@ A
)A B
;B C
} 	
} 
} ‘
WC:\Desenvolvimento\Repos\dotnet-search\src\DotNetSearch.Domain\Common\DomainMessages.cs
	namespace 	
DotNetSearch
 
. 
Domain 
. 
Common $
{ 
public 

static 
class 
DomainMessages &
{ 
public 
static 
DomainMessage #
RequiredField$ 1
=>2 4
new5 8
(8 9
$str9 X
)X Y
;Y Z
public 
static 
DomainMessage #
NotFound$ ,
=>- /
new0 3
(3 4
$str4 U
)U V
;V W
} 
} ˇ
PC:\Desenvolvimento\Repos\dotnet-search\src\DotNetSearch.Domain\Entities\Autor.cs
	namespace 	
DotNetSearch
 
. 
Domain 
. 
Entities &
{ 
public 

class 
Autor 
: 
Entity 
{ 
public 
string 
Nome 
{ 
get  
;  !
set" %
;% &
}' (
public		 
DateTime		 
DataNascimento		 &
{		' (
get		) ,
;		, -
set		. 1
;		1 2
}		3 4
public 
virtual 
IEnumerable "
<" #
Livro# (
>( )
Livros* 0
{1 2
get3 6
;6 7
set8 ;
;; <
}= >
} 
} ˛
TC:\Desenvolvimento\Repos\dotnet-search\src\DotNetSearch.Domain\Entities\Categoria.cs
	namespace 	
DotNetSearch
 
. 
Domain 
. 
Entities &
{ 
public 

class 
	Categoria 
: 
Entity #
{ 
public 
string 
Nome 
{ 
get  
;  !
set" %
;% &
}' (
} 
} Á
QC:\Desenvolvimento\Repos\dotnet-search\src\DotNetSearch.Domain\Entities\Entity.cs
	namespace 	
DotNetSearch
 
. 
Domain 
. 
Entities &
{ 
public 

abstract 
class 
Entity  
{ 
public 
Guid 
Id 
{ 
get 
; 
set !
;! "
}# $
} 
}		 ë
PC:\Desenvolvimento\Repos\dotnet-search\src\DotNetSearch.Domain\Entities\Livro.cs
	namespace 	
DotNetSearch
 
. 
Domain 
. 
Entities &
{ 
public 

class 
Livro 
: 
Entity 
{ 
public 
string 
Titulo 
{ 
get "
;" #
set$ '
;' (
}) *
public		 
string		 
Sinopse		 
{		 
get		  #
;		# $
set		% (
;		( )
}		* +
public

 
string

 
Capa

 
{

 
get

  
;

  !
set

" %
;

% &
}

' (
public 
int 
NumeroPaginas  
{! "
get# &
;& '
set( +
;+ ,
}- .
public 
DateTime 
DataPublicacao &
{' (
get) ,
;, -
set. 1
;1 2
}3 4
public 
Autor 
Autor 
{ 
get  
;  !
set" %
;% &
}' (
public 
Guid 
AutorId 
{ 
get !
;! "
set# &
;& '
}( )
public 
virtual 
IEnumerable "
<" #
LivroCategorias# 2
>2 3

Categorias4 >
{? @
getA D
;D E
setF I
;I J
}K L
} 
} ‡
ZC:\Desenvolvimento\Repos\dotnet-search\src\DotNetSearch.Domain\Entities\LivroCategorias.cs
	namespace 	
DotNetSearch
 
. 
Domain 
. 
Entities &
{ 
public 

class 
LivroCategorias  
:! "
Entity# )
{ 
public 
Livro 
Livro 
{ 
get  
;  !
set" %
;% &
}' (
public 
Guid 
LivroId 
{ 
get !
;! "
set# &
;& '
}( )
public

 
	Categoria

 
	Categoria

 "
{

# $
get

% (
;

( )
set

* -
;

- .
}

/ 0
public 
Guid 
CategoriaId 
{  !
get" %
;% &
set' *
;* +
}, -
} 
} æ
aC:\Desenvolvimento\Repos\dotnet-search\src\DotNetSearch.Domain\Interfaces\ICategoriaRepository.cs
	namespace 	
DotNetSearch
 
. 
Domain 
. 

Interfaces (
{ 
public 

	interface  
ICategoriaRepository )
:* +
IRepository, 7
<7 8
	Categoria8 A
>A B
{C D
}E F
} é
XC:\Desenvolvimento\Repos\dotnet-search\src\DotNetSearch.Domain\Interfaces\IRepository.cs
	namespace 	
DotNetSearch
 
. 
Domain 
. 

Interfaces (
{		 
public

 

	interface

 
IRepository

  
<

  !
TEntity

! (
>

( )
where

* /
TEntity

0 7
:

8 9
Entity

: @
{ 
IUnitOfWork 

UnitOfWork 
{  
get! $
;$ %
}& '
Task 
< 
IEnumerable 
< 
TEntity  
>  !
>! "
Search# )
() *

Expression* 4
<4 5
Func5 9
<9 :
TEntity: A
,A B
boolC G
>G H
>H I
	predicateJ S
)S T
;T U
Task 
< 
IEnumerable 
< 
TEntity  
>  !
>! "
GetAll# )
() *
)* +
;+ ,
Task 
< 
TEntity 
> 
GetById 
( 
Guid "
id# %
)% &
;& '

IQueryable 
< 
TEntity 
> 
Query !
(! "
)" #
;# $
void 
Add 
( 
TEntity 
entity 
)  
;  !
void 
Update 
( 
TEntity 
entity "
)" #
;# $
void 
Remove 
( 
TEntity 
entity "
)" #
;# $
} 
} “
XC:\Desenvolvimento\Repos\dotnet-search\src\DotNetSearch.Domain\Interfaces\IUnitOfWork.cs
	namespace 	
DotNetSearch
 
. 
Domain 
. 

Interfaces (
{ 
public 

	interface 
IUnitOfWork  
{ 
Task 
< 
bool 
> 
Commit 
( 
) 
; 
} 
}		 ê
vC:\Desenvolvimento\Repos\dotnet-search\src\DotNetSearch.Domain\Validators\CategoriaValidators\AddCategoriaValidator.cs
	namespace 	
DotNetSearch
 
. 
Domain 
. 

Validators (
.( )
CategoriaValidators) <
{ 
public 

class !
AddCategoriaValidator &
:' (
CategoriaValidator) ;
{ 
public !
AddCategoriaValidator $
($ %
)% &
{ 	
ValidateRequired 
( 
) 
; 
} 	
}		 
}

 Ë
sC:\Desenvolvimento\Repos\dotnet-search\src\DotNetSearch.Domain\Validators\CategoriaValidators\CategoriaValidator.cs
	namespace 	
DotNetSearch
 
. 
Domain 
. 

Validators (
.( )
CategoriaValidators) <
{ 
public 

abstract 
class 
CategoriaValidator ,
:- .
	Validator/ 8
<8 9
	Categoria9 B
>B C
{ 
	protected		 
void		 
ValidateRequired		 '
(		' (
)		( )
{

 	
RuleFor 
( 
x 
=> 
x 
. 
Nome 
)  
. 
NotEmpty 
( 
) 
. 
WithMessage 
( 
DomainMessages +
.+ ,
RequiredField, 9
.9 :
Format: @
(@ A
$strA G
)G H
.H I
MessageI P
)P Q
;Q R
} 	
} 
} ≥
yC:\Desenvolvimento\Repos\dotnet-search\src\DotNetSearch.Domain\Validators\CategoriaValidators\RemoveCategoriaValidator.cs
	namespace 	
DotNetSearch
 
. 
Domain 
. 

Validators (
.( )
CategoriaValidators) <
{ 
public 

class $
RemoveCategoriaValidator )
:* +
AbstractValidator, =
<= >
Guid> B
>B C
{ 
public		 $
RemoveCategoriaValidator		 '
(		' (
)		( )
{

 	
RuleFor 
( 
x 
=> 
x 
) 
. 
NotEmpty 
( 
) 
. 
WithMessage 
( 
DomainMessages +
.+ ,
RequiredField, 9
.9 :
Format: @
(@ A
$strA E
)E F
.F G
MessageG N
)N O
;O P
} 	
} 
} ﬁ
yC:\Desenvolvimento\Repos\dotnet-search\src\DotNetSearch.Domain\Validators\CategoriaValidators\UpdateCategoriaValidator.cs
	namespace 	
DotNetSearch
 
. 
Domain 
. 

Validators (
.( )
CategoriaValidators) <
{ 
public 

class $
UpdateCategoriaValidator )
:* +
CategoriaValidator, >
{ 
public $
UpdateCategoriaValidator '
(' (
)( )
{ 	

ValidateId 
( 
) 
; 
ValidateRequired 
( 
) 
; 
}		 	
}

 
} ¸
VC:\Desenvolvimento\Repos\dotnet-search\src\DotNetSearch.Domain\Validators\Validator.cs
	namespace 	
DotNetSearch
 
. 
Domain 
. 

Validators (
{ 
public 

abstract 
class 
	Validator #
<# $
T$ %
>% &
:' (
AbstractValidator) :
<: ;
T; <
>< =
where> C
TD E
:F G
EntityH N
{ 
	protected		 
void		 

ValidateId		 !
(		! "
)		" #
{

 	
RuleFor 
( 
x 
=> 
x 
. 
Id 
) 
. 
NotEmpty 
( 
) 
. 
WithMessage 
( 
DomainMessages +
.+ ,
RequiredField, 9
.9 :
Format: @
(@ A
$strA E
)E F
.F G
MessageG N
)N O
;O P
} 	
} 
} 