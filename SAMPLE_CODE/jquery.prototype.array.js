// jquery-prototype.js 1.3.0
// (c) 2012 bok choi.

(function ($) {

    /**
    * jQuery enumerable for plain object. Inherited subclass must implement 'forEach' function.
    * A module that can be mixed in to *any object* in order to provide it with iterator.
    */
    $.Enumerable = {
        /* must be implemented by subclass
        forEach: function(iterator, context) {return this;}          
        */

        /**
        var arr = [4, 12, 7];
        trace(arr.count());  //=> 3
        trace(arr.count(function(item) { return item > 10; }); //=> 1 (12 is only greater than 10)
        */
        count: function (predicate) {
            ///<summary>Returns the size of the enumeration.</summary>
            if (!$.isFunction(predicate)) {
                if (!$.isNull(this.length)) {
                    return this.length;
                }
                predicate = truthy;
            }

            var num = 0;
            this.forEach(function (value, index) {
                if (predicate(value, index)) {
                    num++;
                }
            });
            return num;
        },


        /**
        ['c', 'b', 'a'].all(function(item) { return item.test(/a-z/); ); //=> true
        ['un', 'deux', 'trois'].all(function(item) { return item.length > 3; }); //=> false
        */
        all: function (predicate, context) {
            ///<summary>Determines whether all elements of a sequence satisfy a condition.</summary>
            var result = true;

            context = context || this;
            this.forEach(function (value, index) {
                if (!predicate.call(context, value, index)) {
                    result = false;
                    return false;
                }
            });
            return result;
        },

        /**
        ['c', 'b', 'a'].any(function(item) { return item.test(/a-z/); ); //=> true
        ['un', 'deux', 'trois'].any(function(item) { return item.length > 3; }); //=> true
        */
        any: function (predicate, context) {
            ///<summary>Determines whether any element of a sequence satisfies a condition.</summary>
            var result = false;

            predicate = predicate || truthy;
            context = context || this;
            this.forEach(function (value, index) {
                if (predicate.call(context, value, index)) {
                    result = true;
                    return false;
                }
            });
            return result;
        },

        /**
        var arr = [78, 92, 100, 37, 81];
        arr.average(); //=> 77.6
        arr.average(function(item) { return item/10; }); //=> 7.76
        */
        average: function (selector, context) {
            ///<summary>Computes the average of a sequence of source values</summary>
            var result = 0, num = 0;

            selector = selector || reflect;
            context = context || this;
            this.forEach(function (value, index) {
                result += $.parseNumber(selector.call(context, value, index));
                num += 1;
            });
            return result / Math.max(num, 1);
        },

        /**
        ['c', 'b', 'a'].min(); //=> 'a'
        ['un', 'deux', 'trois'].min(function(item) { return item.length; }); //=> 2
        */
        min: function (selector, context) {
            ///<summary>Returns the minimum value in a sequence</summary>
            var result;

            selector = selector || reflect;
            context = context || this;
            this.forEach(function (value, index) {
                value = selector.call(context, value, index);
                if (result == null || value < result)
                    result = value;
            });
            return result;
        },

        /**
        ['c', 'b', 'a'].max(); //=> 'c'
        ['un', 'deux', 'trois'].max(function(item) { return item.length; }); //=> 5
        */
        max: function (selector, context) {
            ///<summary>Returns the maximum value in a sequence</summary>
            var result;

            selector = selector || reflect;
            context = context || this;
            this.forEach(function (value, index) {
                value = selector.call(context, value, index);
                if (result == null || value > result)
                    result = value;
            });
            return result;
        },

        /**
        var arr = [43.68, 1.25, 583.7, 6.5];
        arr.sum(); //=> 635.13
        arr.sum(function(item) { return item.toFixed(); }); //=> 636
        */
        sum: function (selector, context) {
            ///<summary>Computes the sum of a sequence of values</summary>
            var result = 0;

            selector = selector || reflect;
            context = context || this;
            this.forEach(function (value, index) {
                result += $.parseNumber(selector.call(context, value, index));
            });
            return result;
        },

        /**
        var arr = [43.68, 1.25, 583.7, 6.5];
        arr.sum(); //=> 635.13
        arr.sum(function(item) { return item.toFixed(); }); //=> 636
        */
        sumToDecimal: function (selector, context) {
            ///<summary>Computes the sum of a sequence of values</summary>
            var result = new Decimal(0);
            var tmp = new Decimal(0);

            selector = selector || reflect;
            context = context || this;
            this.forEach(function (value, index) {
                tmp = new Decimal(selector.call(context, value, index) || 0);
                tmp = tmp.isNaN() ? new Decimal(0) : tmp;
                result = result.plus(tmp);
            });
            return result;
        },

        /**        
        [1, 2, '3', '4', '5'].contains(3); //=> true ('3' == 3)
        ['hello', 'world'].contains('HELLO'); //=> false ('hello' != 'HELLO')
        ['hello', 'world'].contains(function(item) { //=> true
        return item.indexOf("lo") != -1; // item is 'hello'
        });
        */
        contains: function (predicate, context) {
            ///<summary>Determines whether a sequence contains a specified element or not</summary>
            var found = false;

            if (!$.isFunction(predicate)) {
                var item = predicate;

                if ($.isFunction(this.indexOf)) {
                    return (this.indexOf(item) != -1);
                }
                predicate = function (val) { return val == item; };
            }
            
            context = context || this;
            this.forEach(function (value, index) {
                if (predicate.call(context, value, index)) {
                    found = true;
                    return false;
                }
            });
            return found;
        },

        /**        
        var arr = [21, 46, 46, 55, 17, 21, 55, 55];
        arr.distinct(); //=> [21, 46, 55, 17]
        
        var fruits1 = [ { Name: "apple", Code: 9 },
        { Name: "orange", Code: 4 },
        { Name: "apple", Code: 19 },
        { Name: "lemon", Code: 12 } ];
        var a1 = fruits1.distinct(function(item) { return item.Name; }); //=> apple whose code is 19 is remove
        */
        distinct: function (keySelector, valueSelector, context) {
            ///<summary>Returns distinct elements from a sequence</summary>

            var hash = new $.HashSet();

            keySelector = keySelector || reflect;
            valueSelector = valueSelector || reflect;
            context = context || this;

            return this.select(function (value) {
                var key = keySelector(value);
                if (hash.set(key)) {
                    return valueSelector(value);
                }
            });
        },

        /**        
        var arr1 = [2.0, 2.1, 2.2, 2.3, 2.4, 2.5];
        var arr2 = [2.2, 2.5];
        var onlyInFirstSet = arr1.except(arr2); //=> [2.0, 2.1, 2.3, 2.4]
        
        var fruits1 = [ { Name: "apple", Code: 9 },
        { Name: "orange", Code: 4 },
        { Name: "apple", Code: 19 }]
        var fruits2 = [ { Name: "apple", Code: 12 } ];
        var onlyInFirstSet = fruits1.except(fruits2, function(item) { return item.Name; }); //=> [{ Name: "orange", Code: 4 }, { Name: "apple", Code: 19 }]
        */
        except: function (second, keySelector, context) {
            ///<summary>Produces the set difference of two sequences</summary>

            var results = [];
            var hash = new $.HashSet();

            keySelector = keySelector || reflect;
            context = context || this;
            second.forEach(function (value) {
                hash.set(keySelector(value));
            });

            this.forEach(function (value) {
                var key = keySelector(value);
                if (!hash.has(key)) {
                    hash.set(key);
                    results.push(value);
                }
            });
            return results;
        },

        /**        
        var arr1 = [44, 26, 92, 30, 71, 38];
        var arr2 = [39, 59, 83, 47, 26, 4, 30];
        var both = arr1.intersect(arr2); //=> [26, 30]
        
        var fruits1 = [ { Name: "apple", Code: 9 },
        { Name: "orange", Code: 4 },
        { Name: "mango", Code: 19 }]
        var fruits2 = [ { Name: "apple", Code: 12 } ];
        var both = fruits1.intersect(fruits2, function(item) { return item.Name; }); //=> [{ Name: "apple", Code: 9 }]
        */
        intersect: function (second, keySelector, context) {
            ///<summary>Produces the set intersection of two sequences</summary>

            var results = [];
            var hash = new $.HashSet();

            keySelector = keySelector || reflect;
            context = context || this;
            second.forEach(function (value) {
                hash.set(keySelector(value));
            });

            this.forEach(function (value) {
                if (hash.unset(keySelector(value))) {
                    results.push(value);
                }
            });
            return results;
        },

        /**        
        var arr1 = [44, 26, 92];
        var arr2 = [30, 92, 26];
        var union = arr1.union(arr2); //=> [44, 26, 92, 30]
        
        var fruits1 = [ { Name: "apple", Code: 9 },
        { Name: "orange", Code: 4 },
        { Name: "mango", Code: 19 }]
        var fruits2 = [ { Name: "apple", Code: 12 },
        { Name: "mango", Code: 15 } ];
        var union = fruits1.union(fruits2, function(item) { return item.Name; }); 
        //=> [{ Name: "apple", Code: 9 },{ Name: "orange", Code: 4 },{ Name: "mango", Code: 19 } ]
        */
        union: function (second, keySelector, context) {
            ///<summary>Produces the set union of two sequences</summary>

            var results = [],
                hash = new $.HashSet(),
                key;

            keySelector = keySelector || reflect;
            context = context || this;
            this.forEach(function (value) {
                key = keySelector(value);
                if (hash.set(key)) {
                    results.push(value);
                }
            });

            second.forEach(function (value) {
                key = keySelector(value);
                if (hash.set(key)) {
                    results.push(value);
                }
            });
            return results;
        },

        /**
        var arr = ["", "apple", "banana", "orange"];
        arr.trim(); //=> ["apple", "banana", "orange"];
        arr.trim(function(item) { return item.test(/an/); }); //=> ["apple"]
        */
        trim: function (predicate, context) {
            ///<summary>Remove the element in the specified condition.</summary>

            if (!predicate) {
                predicate = function (value) { return $.isEmpty(value); }
            }

            context = context || this;
            return this.select(function (value, index) {
                if (!predicate.call(context, value, index)) {
                    return value;
                }
            });
        },

        /**
        ['Hitch', "Hiker's", 'Guide', 'to', 'the', 'Galaxy'].select(function(s) { //=> ['H', 'H', 'G', 'T', 'T', 'G']
        return s.charAt(0).toUpperCase();
        });

        [1,2,3,4,5].select(function(n) {   //=> [1, 4, 9, 16, 25]
        return n * n;
        });
        */
        select: function (predicate, selector, context) {
            ///<summary>Projects each element of a sequence into a new form.</summary>

            var results = [];

            if (!$.isFunction(selector)) {
                context = selector || this;
                selector = predicate;
                predicate = null;
            }

            predicate = predicate || truthy;
            selector = selector || reflect;
            context = context || this;

            this.forEach(function (value, index) {
                if (predicate.call(context, value, index)) {
                    value = selector.call(context, value, index);
                    if (!$.isNull(value)) {
                        results.push(value);
                    }
                }
            });
            return results;
        },

        /**
        var arr = [1, 'two', 3, 'four', 5];       
        arr.where(function(v) { //=> ['two', 'four']
        if (typeof v == "string") return true;
        });
         
        arr.where($.isString); //=> ['two', 'four']
        arr.where(function(n) { return $.isNumber(n); }); //=> [1, 3, 5]
        */
        where: function (predicate, context) {
            ///<summary>Returns all the elements for which the iterator returned a truthy value.</summary>

            var results = [];

            context = context || this;
            this.forEach(function (value, index) {
                if (predicate.call(context, value, index))
                    results.push(value);
            });
            return results;
        },

        /**
        var arr = [1, 7, -2, -4, 5];
        arr.find(function(n) { return n < 0; }); //=> -2
        */
        find: function (predicate, context) {
            ///<summary>Returns the first element for which the iterator returns a truthy value.</summary>

            var result;

            context = context || this;
            this.forEach(function (value, index) {
                if (predicate.call(context, value, index)) {
                    result = value;
                    return false;
                }
            });
            return result;
        },

        /**
        var arr = [1, 7, -2, -4, 5];
        arr.first(); //=> 1
        arr.first(function(n) { return n < 0; }); //=> -2
        */
        first: function (predicate, context) {
            ///<summary>Returns the first element in a sequence that satisfies a specified condition.</summary>

            var result;

            if (!predicate && $.isArray(this)) {
                return this.length > 0 ? this[0] : null;
            }

            predicate = predicate || truthy;
            context = context || this;
            this.forEach(function (value, index) {
                if (predicate.call(context, value, index)) {
                    result = value;
                    return true;
                }
            });
            return result;
        },

        /**
        var arr = [1, 7, -2, -4, 5];
        arr.last(); //=> 5
        arr.last(function(n) { return n < 0; }); //=> -4
        */
        last: function (predicate, context) {
            ///<summary>Returns the last element in a sequence that satisfies a specified condition.</summary>

            var result;

            if (!predicate && $.isArray(this)) {
                return this.length > 0 ? this[this.length - 1] : null;
            }

            predicate = predicate || truthy;
            context = context || this;
            this.forEach(function (value, index) {
                if (predicate.call(context, value, index)) {
                    result = value;
                }
            });
            return result;
        },

        /**
        var arr = [1, 7, -2, -4, 5];
        arr.element(2); //=> -2
        arr.element(-2); //=> -4
        */
        element: function (pos) {
            ///<summary>Returns the element in a sequence that a specified position.</summary>

            var result;

            if (pos < 0) {
                pos = this.count() + pos;
            }

            if ($.isArray(this)) {
                return this[pos];
            }

            this.forEach(function (value, index) {
                if (index == pos) {
                    result = value;
                    return false; // break forEach
                }
            });
            return result;
        },

        /**
        var grades = [ 59, 82, 70, 56, 92, 98, 85 ];
        lowerGrades = grades.skip(3); //=> [56, 92, 98, 85]
        lowerGrades = grades.skip(function(item) { return item < 90; }); //=> [92, 98, 85]
        */
        skip: function (predicate, context) {
            ///<summary>Bypasses elements in a sequence as long as a specified condition is true and then returns the remaining elements</summary>

            var result = [],
                hasfunc = $.isFunction(predicate),
                noskip = false;

            context = context || this;
            this.forEach(function (value, index) {
                if (!noskip) {
                    if (hasfunc && !predicate.call(context, value, index)) {
                        noskip = true;
                    }
                    else if (predicate <= index) {
                        noskip = true;
                    }
                }

                if (noskip) {
                    result.push(value);
                }

                noskip = false;
            });
            return result;
        },

        /**
        var grades = [ 59, 82, 70, 56, 92, 98, 85 ];
        lowerGrades = grades.take(3); //=> [59, 82, 70]
        lowerGrades = grades.take(function(item) { return item < 90; }); //=> [59, 82, 70, 56]
        */
        take: function (predicate, context) {
            ///<summary>Processes elements in a sequence as long as a specified condition is true and then returns the remaining elements</summary>

            var result = [],
                hasfunc = $.isFunction(predicate);

            context = context || this;
            this.forEach(function (value, index) {
                if (hasfunc && !predicate.call(context, value, index)) {
                    return false;
                }
                else if (predicate < index) {
                    return false;
                }

                result.push(value);
            });
            return result;
        },

        /**        
        [1, 3, 2, 1].uniq(); //=> [1, 2, 3]
        ['A', 'a'].uniq(); //=> ['A', 'a'] (because String comparison is case-sensitive)
        ['A', 'a'].uniq(function(val) { //=> ['A']
        return val.toLowerCase();
        });
        */
        uniq: function (keySelector, context) {
            ///<summary>Produces a duplicate-free version of a new array.</summary>

            keySelector = keySelector || reflect;
            context = context || this;
            return this.select(function (value, index) {
                if (index == 0 || !this.contains(keySelector.call(context, value))) {
                    return true;
                }
            });
        },

        /**
        ['hello', 'world'].invoke('toUpperCase'); //=> ['HELLO', 'WORLD']
        ['hello', 'world'].invoke('substring', 0, 3); //=> ['hel', 'wor']
        */
        invoke: function (method) {
            ///<summary>Invokes the same method, with the same arguments, for all items in a collection.</summary>
            var args = this.slice.call(arguments, 1);

            return this.select(function (value) {
                return value[method].apply(value, args);
            });
        },

        /**
        ['hello', 'world', 'this', 'is', 'nice'].pluck('length'); //=> [5, 5, 4, 2, 4]
        var a = [{id:1, name='A'}, {id:2, name='B'}, {id:3, name='C'}];
        var b = a.pluck('name'); //=> ['A', 'B', 'C']
        var b = a.pluck('name', function(v) { //=> ['Aa', 'Bb', 'Cc']
        return v + v.toLowerCase(); 
        }); 
        */
        pluck: function (key, iterator, context) {
            ///<summary>Translate all items in an array and fetching a property.</summary>

            iterator = iterator || reflect;
            context = context || this;
            return this.select(function (value, index) {
                return iterator.call(context, value[key], index);
            });
        },

        /**
        // Get all strings containing a repeated letter
        ['hello', 'world', 'this', 'is', 'cool'].grep(/(.)\1/); //=> ['hello', 'cool']
        ['hello', 'world', 'this', 'is', 'cool'].grep(/(.)\1/, function(v) { //=> ['helloX', 'coolX']
        return v+'X';
        });
        */
        grep: function (filter, selector, context) {
            ///<summary>Returns an array containing all of the elements for which the given filter returns a truthy value</summary>

            if ($.isString(filter)) {
                filter = new RegExp(filter.escapeRegExp());
            }
            return this.select(filter.test, selector, context);
        },

        /**
        [1.3, 2.1, 2.4].groupBy(function(v) { //=> {1: [1.3], 2: [2.1, 2.4]}
        return Math.floor(v);
        });
        ['one', 'two', 'three'].groupBy('length'); //=> {3: ["one", "two"], 5: ["three"]}
        */
        groupBy: function (keySelector, context) {
            ///<summary>Splits a collection into sets, grouped by the result of running each value through iterator.</summary>

            var result = {},
                prop = keySelector;

            if (!$.isFunction(keySelector)) {
                keySelector = function (value) { return value[prop]; };
            }

            context = context || this;
            this.forEach(function (value, index) {
                var key = keySelector.call(context, value, index);
                (result[key] || (result[key] = [])).push(value);
            });
            return result;
        },

        /**
        ['hello', 'world', 'this', 'is', 'nice'].sortBy(function(s) { //=> ['is', 'nice', 'this', 'world', 'hello']
        return s.length;
        });
        ['three', 'two', 'one'].sortBy('length'); //=> ['one', 'two', 'three']
        */
        sortBy: function (keySelector, context) {
            ///<summary>Creates a custom-sorted array of the elements based on the criteria computed.</summary>

            var prop = keySelector;

            if (!$.isFunction(keySelector)) {
                keySelector = function (value) { return value[prop]; };
            }

            context = context || this;
            return this.select(function (value, index) {
                return {
                    value: value,
                    criteria: keySelector.call(context, value, index)
                };
            })
            .sort(function (left, right) {
                var a = left.criteria, b = right.criteria;
                return a < b ? -1 : a > b ? 1 : 0;
            })
            .pluck('value');
        },

        toArray: function () {
            ///<summary>Returns an array from the elements of the enumeration.</summary>
            return this.select();
        }
    };


    /**
    * Static funtion for Array object
    */
    $.extend(Array, {
        /**
        * var arr1 = Array.range(0, 5); //=> [0, 1, 2, 3, 4, 5]
        * var arr2 = Array.range(5, 0, -1); //=> [5, 4, 3, 2, 1, 0]
        */
        range: function (start, stop, step) {
            ///<summary>Returns a new array by specified range</summary>

            if (arguments.length <= 1) {
                stop = start || 0;
                start = 0;
            }
            step = arguments[2] || 1;

            var len = Math.max(Math.ceil((stop - start) / step), 0),
                idx = 0,
                range = [];

            while (idx++ < len) {
                range.push(start);
                start += step;
            }

            return range;
        }
    });

    /**
    * Extend Array prototypes
    */
    $.extend(Array.prototype, $.Enumerable, {
        /* required for Enumerator */
        forEach: Array.prototype.forEach || function (iterator, context) {
            ///<summary>Executes a provided function once per array element. </summary>
            for (var i = 0, len = this.length; i < len; i++) {
                if (iterator.call(context, this[i], i) === false)
                    break;
            }
            return this;
        },

        /**
        * var arr = [];
        * arr.add('first');
        * arr.add('second', 'third', 'last'); //=> ['first, second, third, last']
        */
        add: function () {
            ///<summary>Add a item or items into current array</summary>
            for (var i = 0, len = arguments.length; i < len; i++) {
                var item = arguments[i];
                this.push(item);
            }

            return this;
        },

        /**
        * var arr = [];
        * var trace = false;
        * arr.add('first').addif(trace, 'second', 'third').add('last'); //=> [first, third, last]
        * arr.addif(function(me) { return !me.contains('second'); }, 'second'); //=> [first, third, last, second]
        */
        addif: function (predicate, trueBlock, falseBlock) {
            ///<summary>Conditional add item into array</summary>

            if ($.isFunction(predicate)) {
                predicate = predicate.call(this);
            }

            if (predicate && !$.isEmpty(predicate)) {
                if ($.isArray(trueBlock)) {
                    $.merge(this, trueBlock);
                }
                else {
                    this.push(trueBlock);
                }
            }
            else if (falseBlock != null) {
                if ($.isArray(falseBlock)) {
                    $.merge(this, falseBlock);
                }
                else {
                    this.push(falseBlock);
                }
            }

            return this;
        },

        /**
        var arr = [];
        arr.add('first').add('second'); //=> [first, second]
        arr.addifUnique('second'); //=> nothing changed because 'second' already is in the list.
        */
        addifUnique: function (item) {
            ///<summary>Adds an item to the list if it isn't already in the list</summary>
            this.addif(function (me) { return !me.contains(item); }, item);
        },

        /**
        * var arr = [], curr = 'Today';
        * arr.append('{0} is {1:MM/dd/yyyy}', curr, new Date()).add('has gone').join(' '); //=> 'Today is 11/21/2011 has gone'
        */
        append: function (format, args) {
            ///<summary>Add a item by formatted string</summary>
            this.push(String.format.apply(null, arguments));
            return this;
        },

        /**
        var arr = ["apple", "banana", "orange"];
        arr.insert(2, "mango", "kiwi", "blueberry"); //=> ["apple", "banana", "mango", "kiwi", "blueberry", "orange"] 
        */
        insert: function (index, args) {
            ///<summary>Inserts the element in the specified position and return array for chaning</summary>

            for (var i = 1, len = arguments.length; i < len; i++) {
                var item = arguments[i];
                this.splice(index++, 0, item);
            }
            return this;
        },

        /**
        var arr = ["apple", "banana", "orange", "blueberry", "blackberry"];
        arr.remove(1); //=> ["apple", "orange" , "blueberry", "blackberry" ] 
        arr.remove(function(item) { return item.indexOf("berry") != -1; }); //=> ["apple", "orange"]
        */
        remove: function (at, context) {
            ///<summary>Removes the element in the specified position or specified condition.</summary>

            var predicate = at;
            if (!$.isFunction(predicate)) { // remove at index
                this.splice(at, 1);
                return this;
            }

            for (var i = 0; i < this.length; i++) {
                if (predicate.call(context, this[i], i)) {
                    this.splice(i--, 1);
                }
            }
            return this;
        },

        replace: function (at, item) {
            ///<summary>replaces an item in a collection</summary>

            this.splice(at, 1, item);
            return this;
        },

        /**
        var a = ['frank', ['bob', 'lisa'], ['jill', ['tom', 'sally']]];
        var b = a.flatten(); //=> ['frank', 'bob', 'lisa', 'jill', 'tom', 'sally']
        */
        flatten: function () {
            ///<summary>Returns a new flattened (one-dimensional) copy of the array</summary>
            return Array.prototype.concat.apply([], this);
        },

        clear: function () {
            ///<summary>Clears the array</summary>
            this.length = 0;
            return this;
        },

        clone: function (isDeep) {
            ///<summary>Returns a new array that has same elements of current array</summary>
            if (!isDeep) {
                return this.slice.call(this, 0);
            }
            else {
                var results = [];
                $.each(this, function (index, value) {
                    results.push($.extend({}, value));
                });
                return results;
            }
        },   

        toJSON: function () {     
            ///<summary>Returns a json typed string</summary>

            var results = [];

            $.each(this, function (index, value) {
                var object = Object.toJSON(value);
                if (!$.isNull(object))
                    results.push(object);
            });
            return '[' + results.join(', ') + ']';
        }

    });

})(jQuery);
