#CartManager
------
##Installation
Using NuGet

`PM> Install-Package CartManager`

##Getting started
This library was tought to work as a cart for an E-Commerce but because it's completly generic, it can store anything.

This library is intended to be used with .Net 4.5.1, because is what is supported by IDistributedCache by today.

In your project startup you need to configure which Cache are you going to use as described in the link that follows [ IDistributedCache Setup ] (https://docs.asp.net/en/latest/performance/caching/distributed.html)

Then, where you want to use it just import the namespace CacheManager.Implementation and add this code to your constructor

```C#
using Microsoft.Extensions.Caching.Distributed;
using CartManager.Implementation;

namespace Example
{
    public class Constructor
    {
        private readonly _cart;
        public Constructor(IDistributedCache cache)
        {
            _cart = new Cart<YourClassToStore>(cache);
        }
    }
}
```

This will pass the cache as a dependency.

---
##Usage
Note: All methods on are async.

###Setting a value

```C#
await _cart.Set(IdOfTheUser)
```

###Retriving a Value

```C#
var cart = await _cart.Get(IdOfTheUser)
```


###Updating a cart
```C#
await _cart.Update(newCart, IdOfTheUser)
```

###Delete a cart
```C#
await _cart.Delete(IdOfTheUser)
```