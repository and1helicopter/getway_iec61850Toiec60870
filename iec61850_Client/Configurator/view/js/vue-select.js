!function (t, e) {
    "object" == typeof exports && "object" == typeof module ? module.exports = e() : "function" == typeof define && define.amd ? define([], e) : "object" == typeof exports ? exports.VueSelect = e() : t.VueSelect = e()
}(this, function () {
    return function (t) {
        function e(r) {
            if (n[r]) return n[r].exports;
            var o = n[r] = {exports: {}, id: r, loaded: !1};
            return t[r].call(o.exports, o, o.exports, e), o.loaded = !0, o.exports
        }

        var n = {};
        return e.m = t, e.c = n, e.p = "/", e(0)
    }([function (t, e, n) {
        "use strict";

        function r(t) {
            return t && t.__esModule ? t : {default: t}
        }

        Object.defineProperty(e, "__esModule", {value: !0}), e.mixins = e.VueSelect = void 0;
        var o = n(83), i = r(o), a = n(42), s = r(a);
        e.default = i.default, e.VueSelect = i.default, e.mixins = s.default
    }, function (t, e) {
        var n = t.exports = "undefined" != typeof window && window.Math == Math ? window : "undefined" != typeof self && self.Math == Math ? self : Function("return this")();
        "number" == typeof __g && (__g = n)
    }, function (t, e, n) {
        t.exports = !n(9)(function () {
            return 7 != Object.defineProperty({}, "a", {
                get: function () {
                    return 7
                }
            }).a
        })
    }, function (t, e) {
        var n = {}.hasOwnProperty;
        t.exports = function (t, e) {
            return n.call(t, e)
        }
    }, function (t, e, n) {
        var r = n(11), o = n(33), i = n(25), a = Object.defineProperty;
        e.f = n(2) ? Object.defineProperty : function (t, e, n) {
            if (r(t), e = i(e, !0), r(n), o) try {
                return a(t, e, n)
            } catch (t) {
            }
            if ("get" in n || "set" in n) throw TypeError("Accessors not supported!");
            return "value" in n && (t[e] = n.value), t
        }
    }, function (t, e) {
        var n = t.exports = {version: "2.5.2"};
        "number" == typeof __e && (__e = n)
    }, function (t, e, n) {
        var r = n(4), o = n(14);
        t.exports = n(2) ? function (t, e, n) {
            return r.f(t, e, o(1, n))
        } : function (t, e, n) {
            return t[e] = n, t
        }
    }, function (t, e, n) {
        var r = n(59), o = n(16);
        t.exports = function (t) {
            return r(o(t))
        }
    }, function (t, e, n) {
        var r = n(23)("wks"), o = n(15), i = n(1).Symbol, a = "function" == typeof i, s = t.exports = function (t) {
            return r[t] || (r[t] = a && i[t] || (a ? i : o)("Symbol." + t))
        };
        s.store = r
    }, function (t, e) {
        t.exports = function (t) {
            try {
                return !!t()
            } catch (t) {
                return !0
            }
        }
    }, function (t, e) {
        t.exports = function (t) {
            return "object" == typeof t ? null !== t : "function" == typeof t
        }
    }, function (t, e, n) {
        var r = n(10);
        t.exports = function (t) {
            if (!r(t)) throw TypeError(t + " is not an object!");
            return t
        }
    }, function (t, e, n) {
        var r = n(1), o = n(5), i = n(56), a = n(6), s = "prototype", u = function (t, e, n) {
            var l, c, f, p = t & u.F, d = t & u.G, h = t & u.S, b = t & u.P, v = t & u.B, g = t & u.W,
                y = d ? o : o[e] || (o[e] = {}), m = y[s], x = d ? r : h ? r[e] : (r[e] || {})[s];
            d && (n = e);
            for (l in n) c = !p && x && void 0 !== x[l], c && l in y || (f = c ? x[l] : n[l], y[l] = d && "function" != typeof x[l] ? n[l] : v && c ? i(f, r) : g && x[l] == f ? function (t) {
                var e = function (e, n, r) {
                    if (this instanceof t) {
                        switch (arguments.length) {
                            case 0:
                                return new t;
                            case 1:
                                return new t(e);
                            case 2:
                                return new t(e, n)
                        }
                        return new t(e, n, r)
                    }
                    return t.apply(this, arguments)
                };
                return e[s] = t[s], e
            }(f) : b && "function" == typeof f ? i(Function.call, f) : f, b && ((y.virtual || (y.virtual = {}))[l] = f, t & u.R && m && !m[l] && a(m, l, f)))
        };
        u.F = 1, u.G = 2, u.S = 4, u.P = 8, u.B = 16, u.W = 32, u.U = 64, u.R = 128, t.exports = u
    }, function (t, e, n) {
        var r = n(38), o = n(17);
        t.exports = Object.keys || function (t) {
            return r(t, o)
        }
    }, function (t, e) {
        t.exports = function (t, e) {
            return {enumerable: !(1 & t), configurable: !(2 & t), writable: !(4 & t), value: e}
        }
    }, function (t, e) {
        var n = 0, r = Math.random();
        t.exports = function (t) {
            return "Symbol(".concat(void 0 === t ? "" : t, ")_", (++n + r).toString(36))
        }
    }, function (t, e) {
        t.exports = function (t) {
            if (void 0 == t) throw TypeError("Can't call method on  " + t);
            return t
        }
    }, function (t, e) {
        t.exports = "constructor,hasOwnProperty,isPrototypeOf,propertyIsEnumerable,toLocaleString,toString,valueOf".split(",")
    }, function (t, e) {
        t.exports = {}
    }, function (t, e) {
        t.exports = !0
    }, function (t, e) {
        e.f = {}.propertyIsEnumerable
    }, function (t, e, n) {
        var r = n(4).f, o = n(3), i = n(8)("toStringTag");
        t.exports = function (t, e, n) {
            t && !o(t = n ? t : t.prototype, i) && r(t, i, {configurable: !0, value: e})
        }
    }, function (t, e, n) {
        var r = n(23)("keys"), o = n(15);
        t.exports = function (t) {
            return r[t] || (r[t] = o(t))
        }
    }, function (t, e, n) {
        var r = n(1), o = "__core-js_shared__", i = r[o] || (r[o] = {});
        t.exports = function (t) {
            return i[t] || (i[t] = {})
        }
    }, function (t, e) {
        var n = Math.ceil, r = Math.floor;
        t.exports = function (t) {
            return isNaN(t = +t) ? 0 : (t > 0 ? r : n)(t)
        }
    }, function (t, e, n) {
        var r = n(10);
        t.exports = function (t, e) {
            if (!r(t)) return t;
            var n, o;
            if (e && "function" == typeof(n = t.toString) && !r(o = n.call(t))) return o;
            if ("function" == typeof(n = t.valueOf) && !r(o = n.call(t))) return o;
            if (!e && "function" == typeof(n = t.toString) && !r(o = n.call(t))) return o;
            throw TypeError("Can't convert object to primitive value")
        }
    }, function (t, e, n) {
        var r = n(1), o = n(5), i = n(19), a = n(27), s = n(4).f;
        t.exports = function (t) {
            var e = o.Symbol || (o.Symbol = i ? {} : r.Symbol || {});
            "_" == t.charAt(0) || t in e || s(e, t, {value: a.f(t)})
        }
    }, function (t, e, n) {
        e.f = n(8)
    }, function (t, e) {
        "use strict";
        t.exports = {
            props: {
                loading: {type: Boolean, default: !1},
                onSearch: {
                    type: Function, default: function (t, e) {
                    }
                }
            }, data: function () {
                return {mutableLoading: !1}
            }, watch: {
                search: function () {
                    this.search.length > 0 && (this.onSearch(this.search, this.toggleLoading), this.$emit("search", this.search, this.toggleLoading))
                }, loading: function (t) {
                    this.mutableLoading = t
                }
            }, methods: {
                toggleLoading: function () {
                    var t = arguments.length > 0 && void 0 !== arguments[0] ? arguments[0] : null;
                    return null == t ? this.mutableLoading = !this.mutableLoading : this.mutableLoading = t
                }
            }
        }
    }, function (t, e) {
        "use strict";
        t.exports = {
            watch: {
                typeAheadPointer: function () {
                    this.maybeAdjustScroll()
                }
            }, methods: {
                maybeAdjustScroll: function () {
                    var t = this.pixelsToPointerTop(), e = this.pixelsToPointerBottom();
                    return t <= this.viewport().top ? this.scrollTo(t) : e >= this.viewport().bottom ? this.scrollTo(this.viewport().top + this.pointerHeight()) : void 0
                }, pixelsToPointerTop: function t() {
                    var t = 0;
                    if (this.$refs.dropdownMenu) for (var e = 0; e < this.typeAheadPointer; e++) t += this.$refs.dropdownMenu.children[e].offsetHeight;
                    return t
                }, pixelsToPointerBottom: function () {
                    return this.pixelsToPointerTop() + this.pointerHeight()
                }, pointerHeight: function () {
                    var t = !!this.$refs.dropdownMenu && this.$refs.dropdownMenu.children[this.typeAheadPointer];
                    return t ? t.offsetHeight : 0
                }, viewport: function () {
                    return {
                        top: this.$refs.dropdownMenu ? this.$refs.dropdownMenu.scrollTop : 0,
                        bottom: this.$refs.dropdownMenu ? this.$refs.dropdownMenu.offsetHeight + this.$refs.dropdownMenu.scrollTop : 0
                    }
                }, scrollTo: function (t) {
                    return this.$refs.dropdownMenu ? this.$refs.dropdownMenu.scrollTop = t : null
                }
            }
        }
    }, function (t, e) {
        "use strict";
        t.exports = {
            data: function () {
                return {typeAheadPointer: -1}
            }, watch: {
                filteredOptions: function () {
                    this.typeAheadPointer = 0
                }
            }, methods: {
                typeAheadUp: function () {
                    this.typeAheadPointer > 0 && (this.typeAheadPointer--, this.maybeAdjustScroll && this.maybeAdjustScroll())
                }, typeAheadDown: function () {
                    this.typeAheadPointer < this.filteredOptions.length - 1 && (this.typeAheadPointer++, this.maybeAdjustScroll && this.maybeAdjustScroll())
                }, typeAheadSelect: function () {
                    this.filteredOptions[this.typeAheadPointer] ? this.select(this.filteredOptions[this.typeAheadPointer]) : this.taggable && this.search.length && this.select(this.search), this.clearSearchOnSelect && (this.search = "")
                }
            }
        }
    }, function (t, e) {
        var n = {}.toString;
        t.exports = function (t) {
            return n.call(t).slice(8, -1)
        }
    }, function (t, e, n) {
        var r = n(10), o = n(1).document, i = r(o) && r(o.createElement);
        t.exports = function (t) {
            return i ? o.createElement(t) : {}
        }
    }, function (t, e, n) {
        t.exports = !n(2) && !n(9)(function () {
            return 7 != Object.defineProperty(n(32)("div"), "a", {
                get: function () {
                    return 7
                }
            }).a
        })
    }, function (t, e, n) {
        "use strict";
        var r = n(19), o = n(12), i = n(39), a = n(6), s = n(3), u = n(18), l = n(61), c = n(21), f = n(67),
            p = n(8)("iterator"), d = !([].keys && "next" in [].keys()), h = "@@iterator", b = "keys", v = "values",
            g = function () {
                return this
            };
        t.exports = function (t, e, n, y, m, x, w) {
            l(n, e, y);
            var S, O, _, j = function (t) {
                    if (!d && t in A) return A[t];
                    switch (t) {
                        case b:
                            return function () {
                                return new n(this, t)
                            };
                        case v:
                            return function () {
                                return new n(this, t)
                            }
                    }
                    return function () {
                        return new n(this, t)
                    }
                }, k = e + " Iterator", P = m == v, C = !1, A = t.prototype, M = A[p] || A[h] || m && A[m], L = M || j(m),
                T = m ? P ? j("entries") : L : void 0, E = "Array" == e ? A.entries || M : M;
            if (E && (_ = f(E.call(new t)), _ !== Object.prototype && _.next && (c(_, k, !0), r || s(_, p) || a(_, p, g))), P && M && M.name !== v && (C = !0, L = function () {
                    return M.call(this)
                }), r && !w || !d && !C && A[p] || a(A, p, L), u[e] = L, u[k] = g, m) if (S = {
                    values: P ? L : j(v),
                    keys: x ? L : j(b),
                    entries: T
                }, w) for (O in S) O in A || i(A, O, S[O]); else o(o.P + o.F * (d || C), e, S);
            return S
        }
    }, function (t, e, n) {
        var r = n(11), o = n(64), i = n(17), a = n(22)("IE_PROTO"), s = function () {
        }, u = "prototype", l = function () {
            var t, e = n(32)("iframe"), r = i.length, o = "<", a = ">";
            for (e.style.display = "none", n(58).appendChild(e), e.src = "javascript:", t = e.contentWindow.document, t.open(), t.write(o + "script" + a + "document.F=Object" + o + "/script" + a), t.close(), l = t.F; r--;) delete l[u][i[r]];
            return l()
        };
        t.exports = Object.create || function (t, e) {
            var n;
            return null !== t ? (s[u] = r(t), n = new s, s[u] = null, n[a] = t) : n = l(), void 0 === e ? n : o(n, e)
        }
    }, function (t, e, n) {
        var r = n(38), o = n(17).concat("length", "prototype");
        e.f = Object.getOwnPropertyNames || function (t) {
            return r(t, o)
        }
    }, function (t, e) {
        e.f = Object.getOwnPropertySymbols
    }, function (t, e, n) {
        var r = n(3), o = n(7), i = n(55)(!1), a = n(22)("IE_PROTO");
        t.exports = function (t, e) {
            var n, s = o(t), u = 0, l = [];
            for (n in s) n != a && r(s, n) && l.push(n);
            for (; e.length > u;) r(s, n = e[u++]) && (~i(l, n) || l.push(n));
            return l
        }
    }, function (t, e, n) {
        t.exports = n(6)
    }, function (t, e, n) {
        var r = n(16);
        t.exports = function (t) {
            return Object(r(t))
        }
    }, function (t, e, n) {
        "use strict";

        function r(t) {
            return t && t.__esModule ? t : {default: t}
        }

        Object.defineProperty(e, "__esModule", {value: !0});
        var o = n(44), i = r(o), a = n(47), s = r(a), u = n(48), l = r(u), c = n(29), f = r(c), p = n(30), d = r(p),
            h = n(28), b = r(h);
        e.default = {
            mixins: [f.default, d.default, b.default],
            props: {
                value: {default: null},
                options: {
                    type: Array, default: function () {
                        return []
                    }
                },
                disabled: {type: Boolean, default: !1},
                maxHeight: {type: String, default: "400px"},
                searchable: {type: Boolean, default: !0},
                multiple: {type: Boolean, default: !1},
                placeholder: {type: String, default: ""},
                transition: {type: String, default: "fade"},
                clearSearchOnSelect: {type: Boolean, default: !0},
                closeOnSelect: {type: Boolean, default: !0},
                label: {type: String, default: "label"},
                getOptionLabel: {
                    type: Function, default: function (t) {
                        return "object" === ("undefined" == typeof t ? "undefined" : (0, l.default)(t)) && this.label && t[this.label] ? t[this.label] : t
                    }
                },
                onChange: {
                    type: Function, default: function (t) {
                        this.$emit("input", t)
                    }
                },
                taggable: {type: Boolean, default: !1},
                tabindex: {type: Number, default: null},
                pushTags: {type: Boolean, default: !1},
                filterable: {type: Boolean, default: !0},
                createOption: {
                    type: Function, default: function (t) {
                        return "object" === (0, l.default)(this.mutableOptions[0]) && (t = (0, s.default)({}, this.label, t)), this.$emit("option:created", t), t
                    }
                },
                resetOnOptionsChange: {type: Boolean, default: !1},
                noDrop: {type: Boolean, default: !1},
                inputId: {type: String},
                dir: {type: String, default: "auto"}
            },
            data: function () {
                return {search: "", open: !1, mutableValue: null, mutableOptions: []}
            },
            watch: {
                value: function (t) {
                    this.mutableValue = t
                }, mutableValue: function (t, e) {
                    this.multiple ? this.onChange ? this.onChange(t) : null : this.onChange && t !== e ? this.onChange(t) : null
                }, options: function (t) {
                    this.mutableOptions = t
                }, mutableOptions: function () {
                    !this.taggable && this.resetOnOptionsChange && (this.mutableValue = this.multiple ? [] : null)
                }, multiple: function (t) {
                    this.mutableValue = t ? [] : null
                }
            },
            created: function () {
                this.mutableValue = this.value, this.mutableOptions = this.options.slice(0), this.mutableLoading = this.loading, this.$on("option:created", this.maybePushTag)
            },
            methods: {
                select: function (t) {
                    this.isOptionSelected(t) ? this.deselect(t) : (this.taggable && !this.optionExists(t) && (t = this.createOption(t)), this.multiple && !this.mutableValue ? this.mutableValue = [t] : this.multiple ? this.mutableValue.push(t) : this.mutableValue = t), this.onAfterSelect(t)
                }, deselect: function (t) {
                    var e = this;
                    if (this.multiple) {
                        var n = -1;
                        this.mutableValue.forEach(function (r) {
                            (r === t || "object" === ("undefined" == typeof r ? "undefined" : (0, l.default)(r)) && r[e.label] === t[e.label]) && (n = r)
                        });
                        var r = this.mutableValue.indexOf(n);
                        this.mutableValue.splice(r, 1)
                    } else this.mutableValue = null
                }, clearSelection: function () {
                    this.mutableValue = this.multiple ? [] : null
                }, onAfterSelect: function (t) {
                    this.closeOnSelect && (this.open = !this.open, this.$refs.search.blur()), this.clearSearchOnSelect && (this.search = "")
                }, toggleDropdown: function (t) {
                    t.target !== this.$refs.openIndicator && t.target !== this.$refs.search && t.target !== this.$refs.toggle && t.target !== this.$el || (this.open ? this.$refs.search.blur() : this.disabled || (this.open = !0, this.$refs.search.focus()))
                }, isOptionSelected: function (t) {
                    var e = this;
                    if (this.multiple && this.mutableValue) {
                        var n = !1;
                        return this.mutableValue.forEach(function (r) {
                            "object" === ("undefined" == typeof r ? "undefined" : (0, l.default)(r)) && r[e.label] === t[e.label] ? n = !0 : "object" === ("undefined" == typeof r ? "undefined" : (0, l.default)(r)) && r[e.label] === t ? n = !0 : r === t && (n = !0)
                        }), n
                    }
                    return this.mutableValue === t
                }, onEscape: function () {
                    this.search.length ? this.search = "" : this.$refs.search.blur()
                }, onSearchBlur: function () {
                    this.clearSearchOnBlur && (this.search = ""), this.open = !1, this.$emit("search:blur")
                }, onSearchFocus: function () {
                    this.open = !0, this.$emit("search:focus")
                }, maybeDeleteValue: function () {
                    if (!this.$refs.search.value.length && this.mutableValue) return this.multiple ? this.mutableValue.pop() : this.mutableValue = null
                }, optionExists: function (t) {
                    var e = this, n = !1;
                    return this.mutableOptions.forEach(function (r) {
                        "object" === ("undefined" == typeof r ? "undefined" : (0, l.default)(r)) && r[e.label] === t ? n = !0 : r === t && (n = !0)
                    }), n
                }, maybePushTag: function (t) {
                    this.pushTags && this.mutableOptions.push(t)
                }
            },
            computed: {
                dropdownClasses: function () {
                    return {
                        open: this.dropdownOpen,
                        single: !this.multiple,
                        searching: this.searching,
                        searchable: this.searchable,
                        unsearchable: !this.searchable,
                        loading: this.mutableLoading,
                        rtl: "rtl" === this.dir,
                        disabled: this.disabled
                    }
                }, clearSearchOnBlur: function () {
                    return this.clearSearchOnSelect && !this.multiple
                }, searching: function () {
                    return !!this.search
                }, dropdownOpen: function () {
                    return !this.noDrop && (this.open && !this.mutableLoading)
                }, searchPlaceholder: function () {
                    if (this.isValueEmpty && this.placeholder) return this.placeholder
                }, filteredOptions: function () {
                    var t = this;
                    if (!this.filterable && !this.taggable) return this.mutableOptions.slice();
                    var e = this.mutableOptions.filter(function (e) {
                        return "object" === ("undefined" == typeof e ? "undefined" : (0, l.default)(e)) && e.hasOwnProperty(t.label) ? e[t.label].toLowerCase().indexOf(t.search.toLowerCase()) > -1 : "object" !== ("undefined" == typeof e ? "undefined" : (0, l.default)(e)) || e.hasOwnProperty(t.label) ? e.toLowerCase().indexOf(t.search.toLowerCase()) > -1 : console.warn('[vue-select warn]: Label key "option.' + t.label + '" does not exist in options object.\nhttp://sagalbot.github.io/vue-select/#ex-labels')
                    });
                    return this.taggable && this.search.length && !this.optionExists(this.search) && e.unshift(this.search), e
                }, isValueEmpty: function () {
                    return !this.mutableValue || ("object" === (0, l.default)(this.mutableValue) ? !(0, i.default)(this.mutableValue).length : !this.mutableValue.length)
                }, valueAsArray: function () {
                    return this.multiple ? this.mutableValue : this.mutableValue ? [this.mutableValue] : []
                }, showClearButton: function () {
                    return !this.multiple && !this.open && null != this.mutableValue
                }
            }
        }
    }, function (t, e, n) {
        "use strict";

        function r(t) {
            return t && t.__esModule ? t : {default: t}
        }

        Object.defineProperty(e, "__esModule", {value: !0});
        var o = n(28), i = r(o), a = n(30), s = r(a), u = n(29), l = r(u);
        e.default = {ajax: i.default, pointer: s.default, pointerScroll: l.default}
    }, function (t, e, n) {
        t.exports = {default: n(49), __esModule: !0}
    }, function (t, e, n) {
        t.exports = {default: n(50), __esModule: !0}
    }, function (t, e, n) {
        t.exports = {default: n(51), __esModule: !0}
    }, function (t, e, n) {
        t.exports = {default: n(52), __esModule: !0}
    }, function (t, e, n) {
        "use strict";

        function r(t) {
            return t && t.__esModule ? t : {default: t}
        }

        e.__esModule = !0;
        var o = n(43), i = r(o);
        e.default = function (t, e, n) {
            return e in t ? (0, i.default)(t, e, {
                value: n,
                enumerable: !0,
                configurable: !0,
                writable: !0
            }) : t[e] = n, t
        }
    }, function (t, e, n) {
        "use strict";

        function r(t) {
            return t && t.__esModule ? t : {default: t}
        }

        e.__esModule = !0;
        var o = n(46), i = r(o), a = n(45), s = r(a),
            u = "function" == typeof s.default && "symbol" == typeof i.default ? function (t) {
                return typeof t
            } : function (t) {
                return t && "function" == typeof s.default && t.constructor === s.default && t !== s.default.prototype ? "symbol" : typeof t
            };
        e.default = "function" == typeof s.default && "symbol" === u(i.default) ? function (t) {
            return "undefined" == typeof t ? "undefined" : u(t)
        } : function (t) {
            return t && "function" == typeof s.default && t.constructor === s.default && t !== s.default.prototype ? "symbol" : "undefined" == typeof t ? "undefined" : u(t)
        }
    }, function (t, e, n) {
        n(73);
        var r = n(5).Object;
        t.exports = function (t, e, n) {
            return r.defineProperty(t, e, n)
        }
    }, function (t, e, n) {
        n(74), t.exports = n(5).Object.keys
    }, function (t, e, n) {
        n(77), n(75), n(78), n(79), t.exports = n(5).Symbol
    }, function (t, e, n) {
        n(76), n(80), t.exports = n(27).f("iterator")
    }, function (t, e) {
        t.exports = function (t) {
            if ("function" != typeof t) throw TypeError(t + " is not a function!");
            return t
        }
    }, function (t, e) {
        t.exports = function () {
        }
    }, function (t, e, n) {
        var r = n(7), o = n(71), i = n(70);
        t.exports = function (t) {
            return function (e, n, a) {
                var s, u = r(e), l = o(u.length), c = i(a, l);
                if (t && n != n) {
                    for (; l > c;) if (s = u[c++], s != s) return !0
                } else for (; l > c; c++) if ((t || c in u) && u[c] === n) return t || c || 0;
                return !t && -1
            }
        }
    }, function (t, e, n) {
        var r = n(53);
        t.exports = function (t, e, n) {
            if (r(t), void 0 === e) return t;
            switch (n) {
                case 1:
                    return function (n) {
                        return t.call(e, n)
                    };
                case 2:
                    return function (n, r) {
                        return t.call(e, n, r)
                    };
                case 3:
                    return function (n, r, o) {
                        return t.call(e, n, r, o)
                    }
            }
            return function () {
                return t.apply(e, arguments)
            }
        }
    }, function (t, e, n) {
        var r = n(13), o = n(37), i = n(20);
        t.exports = function (t) {
            var e = r(t), n = o.f;
            if (n) for (var a, s = n(t), u = i.f, l = 0; s.length > l;) u.call(t, a = s[l++]) && e.push(a);
            return e
        }
    }, function (t, e, n) {
        var r = n(1).document;
        t.exports = r && r.documentElement
    }, function (t, e, n) {
        var r = n(31);
        t.exports = Object("z").propertyIsEnumerable(0) ? Object : function (t) {
            return "String" == r(t) ? t.split("") : Object(t)
        }
    }, function (t, e, n) {
        var r = n(31);
        t.exports = Array.isArray || function (t) {
            return "Array" == r(t)
        }
    }, function (t, e, n) {
        "use strict";
        var r = n(35), o = n(14), i = n(21), a = {};
        n(6)(a, n(8)("iterator"), function () {
            return this
        }), t.exports = function (t, e, n) {
            t.prototype = r(a, {next: o(1, n)}), i(t, e + " Iterator")
        }
    }, function (t, e) {
        t.exports = function (t, e) {
            return {value: e, done: !!t}
        }
    }, function (t, e, n) {
        var r = n(15)("meta"), o = n(10), i = n(3), a = n(4).f, s = 0, u = Object.isExtensible || function () {
            return !0
        }, l = !n(9)(function () {
            return u(Object.preventExtensions({}))
        }), c = function (t) {
            a(t, r, {value: {i: "O" + ++s, w: {}}})
        }, f = function (t, e) {
            if (!o(t)) return "symbol" == typeof t ? t : ("string" == typeof t ? "S" : "P") + t;
            if (!i(t, r)) {
                if (!u(t)) return "F";
                if (!e) return "E";
                c(t)
            }
            return t[r].i
        }, p = function (t, e) {
            if (!i(t, r)) {
                if (!u(t)) return !0;
                if (!e) return !1;
                c(t)
            }
            return t[r].w
        }, d = function (t) {
            return l && h.NEED && u(t) && !i(t, r) && c(t), t
        }, h = t.exports = {KEY: r, NEED: !1, fastKey: f, getWeak: p, onFreeze: d}
    }, function (t, e, n) {
        var r = n(4), o = n(11), i = n(13);
        t.exports = n(2) ? Object.defineProperties : function (t, e) {
            o(t);
            for (var n, a = i(e), s = a.length, u = 0; s > u;) r.f(t, n = a[u++], e[n]);
            return t
        }
    }, function (t, e, n) {
        var r = n(20), o = n(14), i = n(7), a = n(25), s = n(3), u = n(33), l = Object.getOwnPropertyDescriptor;
        e.f = n(2) ? l : function (t, e) {
            if (t = i(t), e = a(e, !0), u) try {
                return l(t, e)
            } catch (t) {
            }
            if (s(t, e)) return o(!r.f.call(t, e), t[e])
        }
    }, function (t, e, n) {
        var r = n(7), o = n(36).f, i = {}.toString,
            a = "object" == typeof window && window && Object.getOwnPropertyNames ? Object.getOwnPropertyNames(window) : [],
            s = function (t) {
                try {
                    return o(t)
                } catch (t) {
                    return a.slice()
                }
            };
        t.exports.f = function (t) {
            return a && "[object Window]" == i.call(t) ? s(t) : o(r(t))
        }
    }, function (t, e, n) {
        var r = n(3), o = n(40), i = n(22)("IE_PROTO"), a = Object.prototype;
        t.exports = Object.getPrototypeOf || function (t) {
            return t = o(t), r(t, i) ? t[i] : "function" == typeof t.constructor && t instanceof t.constructor ? t.constructor.prototype : t instanceof Object ? a : null
        }
    }, function (t, e, n) {
        var r = n(12), o = n(5), i = n(9);
        t.exports = function (t, e) {
            var n = (o.Object || {})[t] || Object[t], a = {};
            a[t] = e(n), r(r.S + r.F * i(function () {
                n(1)
            }), "Object", a)
        }
    }, function (t, e, n) {
        var r = n(24), o = n(16);
        t.exports = function (t) {
            return function (e, n) {
                var i, a, s = String(o(e)), u = r(n), l = s.length;
                return u < 0 || u >= l ? t ? "" : void 0 : (i = s.charCodeAt(u), i < 55296 || i > 56319 || u + 1 === l || (a = s.charCodeAt(u + 1)) < 56320 || a > 57343 ? t ? s.charAt(u) : i : t ? s.slice(u, u + 2) : (i - 55296 << 10) + (a - 56320) + 65536)
            }
        }
    }, function (t, e, n) {
        var r = n(24), o = Math.max, i = Math.min;
        t.exports = function (t, e) {
            return t = r(t), t < 0 ? o(t + e, 0) : i(t, e)
        }
    }, function (t, e, n) {
        var r = n(24), o = Math.min;
        t.exports = function (t) {
            return t > 0 ? o(r(t), 9007199254740991) : 0
        }
    }, function (t, e, n) {
        "use strict";
        var r = n(54), o = n(62), i = n(18), a = n(7);
        t.exports = n(34)(Array, "Array", function (t, e) {
            this._t = a(t), this._i = 0, this._k = e
        }, function () {
            var t = this._t, e = this._k, n = this._i++;
            return !t || n >= t.length ? (this._t = void 0, o(1)) : "keys" == e ? o(0, n) : "values" == e ? o(0, t[n]) : o(0, [n, t[n]])
        }, "values"), i.Arguments = i.Array, r("keys"), r("values"), r("entries")
    }, function (t, e, n) {
        var r = n(12);
        r(r.S + r.F * !n(2), "Object", {defineProperty: n(4).f})
    }, function (t, e, n) {
        var r = n(40), o = n(13);
        n(68)("keys", function () {
            return function (t) {
                return o(r(t))
            }
        })
    }, function (t, e) {
    }, function (t, e, n) {
        "use strict";
        var r = n(69)(!0);
        n(34)(String, "String", function (t) {
            this._t = String(t), this._i = 0
        }, function () {
            var t, e = this._t, n = this._i;
            return n >= e.length ? {value: void 0, done: !0} : (t = r(e, n), this._i += t.length, {value: t, done: !1})
        })
    }, function (t, e, n) {
        "use strict";
        var r = n(1), o = n(3), i = n(2), a = n(12), s = n(39), u = n(63).KEY, l = n(9), c = n(23), f = n(21),
            p = n(15), d = n(8), h = n(27), b = n(26), v = n(57), g = n(60), y = n(11), m = n(10), x = n(7), w = n(25),
            S = n(14), O = n(35), _ = n(66), j = n(65), k = n(4), P = n(13), C = j.f, A = k.f, M = _.f, L = r.Symbol,
            T = r.JSON, E = T && T.stringify, V = "prototype", B = d("_hidden"), F = d("toPrimitive"),
            $ = {}.propertyIsEnumerable, N = c("symbol-registry"), D = c("symbols"), I = c("op-symbols"), R = Object[V],
            z = "function" == typeof L, H = r.QObject, G = !H || !H[V] || !H[V].findChild, U = i && l(function () {
                return 7 != O(A({}, "a", {
                    get: function () {
                        return A(this, "a", {value: 7}).a
                    }
                })).a
            }) ? function (t, e, n) {
                var r = C(R, e);
                r && delete R[e], A(t, e, n), r && t !== R && A(R, e, r)
            } : A, W = function (t) {
                var e = D[t] = O(L[V]);
                return e._k = t, e
            }, J = z && "symbol" == typeof L.iterator ? function (t) {
                return "symbol" == typeof t
            } : function (t) {
                return t instanceof L
            }, K = function (t, e, n) {
                return t === R && K(I, e, n), y(t), e = w(e, !0), y(n), o(D, e) ? (n.enumerable ? (o(t, B) && t[B][e] && (t[B][e] = !1), n = O(n, {enumerable: S(0, !1)})) : (o(t, B) || A(t, B, S(1, {})), t[B][e] = !0), U(t, e, n)) : A(t, e, n)
            }, Y = function (t, e) {
                y(t);
                for (var n, r = v(e = x(e)), o = 0, i = r.length; i > o;) K(t, n = r[o++], e[n]);
                return t
            }, q = function (t, e) {
                return void 0 === e ? O(t) : Y(O(t), e)
            }, Q = function (t) {
                var e = $.call(this, t = w(t, !0));
                return !(this === R && o(D, t) && !o(I, t)) && (!(e || !o(this, t) || !o(D, t) || o(this, B) && this[B][t]) || e)
            }, Z = function (t, e) {
                if (t = x(t), e = w(e, !0), t !== R || !o(D, e) || o(I, e)) {
                    var n = C(t, e);
                    return !n || !o(D, e) || o(t, B) && t[B][e] || (n.enumerable = !0), n
                }
            }, X = function (t) {
                for (var e, n = M(x(t)), r = [], i = 0; n.length > i;) o(D, e = n[i++]) || e == B || e == u || r.push(e);
                return r
            }, tt = function (t) {
                for (var e, n = t === R, r = M(n ? I : x(t)), i = [], a = 0; r.length > a;) !o(D, e = r[a++]) || n && !o(R, e) || i.push(D[e]);
                return i
            };
        z || (L = function () {
            if (this instanceof L) throw TypeError("Symbol is not a constructor!");
            var t = p(arguments.length > 0 ? arguments[0] : void 0), e = function (n) {
                this === R && e.call(I, n), o(this, B) && o(this[B], t) && (this[B][t] = !1), U(this, t, S(1, n))
            };
            return i && G && U(R, t, {configurable: !0, set: e}), W(t)
        }, s(L[V], "toString", function () {
            return this._k
        }), j.f = Z, k.f = K, n(36).f = _.f = X, n(20).f = Q, n(37).f = tt, i && !n(19) && s(R, "propertyIsEnumerable", Q, !0), h.f = function (t) {
            return W(d(t))
        }), a(a.G + a.W + a.F * !z, {Symbol: L});
        for (var et = "hasInstance,isConcatSpreadable,iterator,match,replace,search,species,split,toPrimitive,toStringTag,unscopables".split(","), nt = 0; et.length > nt;) d(et[nt++]);
        for (var rt = P(d.store), ot = 0; rt.length > ot;) b(rt[ot++]);
        a(a.S + a.F * !z, "Symbol", {
            for: function (t) {
                return o(N, t += "") ? N[t] : N[t] = L(t)
            }, keyFor: function (t) {
                if (!J(t)) throw TypeError(t + " is not a symbol!");
                for (var e in N) if (N[e] === t) return e
            }, useSetter: function () {
                G = !0
            }, useSimple: function () {
                G = !1
            }
        }), a(a.S + a.F * !z, "Object", {
            create: q,
            defineProperty: K,
            defineProperties: Y,
            getOwnPropertyDescriptor: Z,
            getOwnPropertyNames: X,
            getOwnPropertySymbols: tt
        }), T && a(a.S + a.F * (!z || l(function () {
            var t = L();
            return "[null]" != E([t]) || "{}" != E({a: t}) || "{}" != E(Object(t))
        })), "JSON", {
            stringify: function (t) {
                for (var e, n, r = [t], o = 1; arguments.length > o;) r.push(arguments[o++]);
                if (n = e = r[1], (m(e) || void 0 !== t) && !J(t)) return g(e) || (e = function (t, e) {
                    if (n && (e = n.call(this, t, e)), !J(e)) return e
                }), r[1] = e, E.apply(T, r)
            }
        }), L[V][F] || n(6)(L[V], F, L[V].valueOf), f(L, "Symbol"), f(Math, "Math", !0), f(r.JSON, "JSON", !0)
    }, function (t, e, n) {
        n(26)("asyncIterator")
    }, function (t, e, n) {
        n(26)("observable")
    }, function (t, e, n) {
        n(72);
        for (var r = n(1), o = n(6), i = n(18), a = n(8)("toStringTag"), s = "CSSRuleList,CSSStyleDeclaration,CSSValueList,ClientRectList,DOMRectList,DOMStringList,DOMTokenList,DataTransferItemList,FileList,HTMLAllCollection,HTMLCollection,HTMLFormElement,HTMLSelectElement,MediaList,MimeTypeArray,NamedNodeMap,NodeList,PaintRequestList,Plugin,PluginArray,SVGLengthList,SVGNumberList,SVGPathSegList,SVGPointList,SVGStringList,SVGTransformList,SourceBufferList,StyleSheetList,TextTrackCueList,TextTrackList,TouchList".split(","), u = 0; u < s.length; u++) {
            var l = s[u], c = r[l], f = c && c.prototype;
            f && !f[a] && o(f, a, l), i[l] = i.Array
        }
    }, function (t, e, n) {
        e = t.exports = n(82)(), e.push([t.id, '.v-select{position:relative};', ""])
    }, function (t, e) {
        t.exports = function () {
            var t = [];
            return t.toString = function () {
                for (var t = [], e = 0; e < this.length; e++) {
                    var n = this[e];
                    n[2] ? t.push("@media " + n[2] + "{" + n[1] + "}") : t.push(n[1])
                }
                return t.join("")
            }, t.i = function (e, n) {
                "string" == typeof e && (e = [[null, e, ""]]);
                for (var r = {}, o = 0; o < this.length; o++) {
                    var i = this[o][0];
                    "number" == typeof i && (r[i] = !0)
                }
                for (o = 0; o < e.length; o++) {
                    var a = e[o];
                    "number" == typeof a[0] && r[a[0]] || (n && !a[2] ? a[2] = n : n && (a[2] = "(" + a[2] + ") and (" + n + ")"), t.push(a))
                }
            }, t
        }
    }, function (t, e, n) {
        n(87);
        var r = n(84)(n(41), n(85), null, null);
        t.exports = r.exports
    }, function (t, e) {
        t.exports = function (t, e, n, r) {
            var o, i = t = t || {}, a = typeof t.default;
            "object" !== a && "function" !== a || (o = t, i = t.default);
            var s = "function" == typeof i ? i.options : i;
            if (e && (s.render = e.render, s.staticRenderFns = e.staticRenderFns), n && (s._scopeId = n), r) {
                var u = s.computed || (s.computed = {});
                Object.keys(r).forEach(function (t) {
                    var e = r[t];
                    u[t] = function () {
                        return e
                    }
                })
            }
            return {esModule: o, exports: i, options: s}
        }
    }, function (t, e) {
        t.exports = {
            render: function () {
                var t = this, e = t.$createElement, n = t._self._c || e;
                return n("div", {
                    staticClass: "dropdown v-select",
                    class: t.dropdownClasses,
                    attrs: {dir: t.dir}
                }, [n("div", {
                    ref: "toggle", class: ["dropdown-toggle", "clearfix"], on: {
                        mousedown: function (e) {
                            e.preventDefault(), t.toggleDropdown(e)
                        }
                    }
                }, [t._l(t.valueAsArray, function (e) {
                    return n("span", {
                        key: e.index,
                        staticClass: "selected-tag"
                    }, [t._t("selected-option", [t._v("\n        " + t._s(t.getOptionLabel(e)) + "\n      ")], null, e), t._v(" "), t.multiple ? n("button", {
                        staticClass: "close",
                        attrs: {disabled: t.disabled, type: "button", "aria-label": "Remove option"},
                        on: {
                            click: function (n) {
                                t.deselect(e)
                            }
                        }
                    }, [n("span", {attrs: {"aria-hidden": "true"}}, [t._v("×")])]) : t._e()], 2)
                }), t._v(" "), n("input", {
                    directives: [{name: "model", rawName: "v-model", value: t.search, expression: "search"}],
                    ref: "search",
                    staticClass: "form-control",
                    style: {width: t.isValueEmpty ? "100%" : "auto"},
                    attrs: {
                        type: "search",
                        autocomplete: "false",
                        disabled: t.disabled,
                        placeholder: t.searchPlaceholder,
                        tabindex: t.tabindex,
                        readonly: !t.searchable,
                        id: t.inputId,
                        "aria-label": "Search for option"
                    },
                    domProps: {value: t.search},
                    on: {
                        keydown: [function (e) {
                            return "button" in e || !t._k(e.keyCode, "delete", [8, 46], e.key) ? void t.maybeDeleteValue(e) : null
                        }, function (e) {
                            return "button" in e || !t._k(e.keyCode, "up", 38, e.key) ? (e.preventDefault(), void t.typeAheadUp(e)) : null
                        }, function (e) {
                            return "button" in e || !t._k(e.keyCode, "down", 40, e.key) ? (e.preventDefault(), void t.typeAheadDown(e)) : null
                        }, function (e) {
                            return "button" in e || !t._k(e.keyCode, "enter", 13, e.key) ? (e.preventDefault(), void t.typeAheadSelect(e)) : null
                        }], keyup: function (e) {
                            return "button" in e || !t._k(e.keyCode, "esc", 27, e.key) ? void t.onEscape(e) : null
                        }, blur: t.onSearchBlur, focus: t.onSearchFocus, input: function (e) {
                            e.target.composing || (t.search = e.target.value)
                        }
                    }
                }), t._v(" "), n("button", {
                    directives: [{
                        name: "show",
                        rawName: "v-show",
                        value: t.showClearButton,
                        expression: "showClearButton"
                    }],
                    staticClass: "clear",
                    attrs: {disabled: t.disabled, type: "button", title: "Clear selection"},
                    on: {click: t.clearSelection}
                }, [n("span", {attrs: {"aria-hidden": "true"}}, [t._v("×")])]), t._v(" "), t.noDrop ? t._e() : n("i", {
                    ref: "openIndicator",
                    staticClass: "open-indicator",
                    attrs: {role: "presentation"}
                }), t._v(" "), t._t("spinner", [n("div", {
                    directives: [{
                        name: "show",
                        rawName: "v-show",
                        value: t.mutableLoading,
                        expression: "mutableLoading"
                    }], staticClass: "spinner"
                }, [t._v("Loading...")])])], 2), t._v(" "), n("transition", {attrs: {name: t.transition}}, [t.dropdownOpen ? n("ul", {
                    ref: "dropdownMenu",
                    staticClass: "dropdown-menu",
                    style: {"max-height": t.maxHeight}
                }, [t._l(t.filteredOptions, function (e, r) {
                    return n("li", {
                        key: r,
                        class: {active: t.isOptionSelected(e), highlight: r === t.typeAheadPointer},
                        on: {
                            mouseover: function (e) {
                                t.typeAheadPointer = r
                            }
                        }
                    }, [n("a", {
                        on: {
                            mousedown: function (n) {
                                n.preventDefault(), t.select(e)
                            }
                        }
                    }, [t._t("option", [t._v("\n          " + t._s(t.getOptionLabel(e)) + "\n        ")], null, e)], 2)])
                }), t._v(" "), t.filteredOptions.length ? t._e() : n("li", {staticClass: "no-options"}, [t._t("no-options", [t._v("Sorry, no matching options.")])], 2)], 2) : t._e()])], 1)
            }, staticRenderFns: []
        }
    }, function (t, e, n) {
        function r(t, e) {
            for (var n = 0; n < t.length; n++) {
                var r = t[n], o = f[r.id];
                if (o) {
                    o.refs++;
                    for (var i = 0; i < o.parts.length; i++) o.parts[i](r.parts[i]);
                    for (; i < r.parts.length; i++) o.parts.push(u(r.parts[i], e))
                } else {
                    for (var a = [], i = 0; i < r.parts.length; i++) a.push(u(r.parts[i], e));
                    f[r.id] = {id: r.id, refs: 1, parts: a}
                }
            }
        }

        function o(t) {
            for (var e = [], n = {}, r = 0; r < t.length; r++) {
                var o = t[r], i = o[0], a = o[1], s = o[2], u = o[3], l = {css: a, media: s, sourceMap: u};
                n[i] ? n[i].parts.push(l) : e.push(n[i] = {id: i, parts: [l]})
            }
            return e
        }

        function i(t, e) {
            var n = h(), r = g[g.length - 1];
            if ("top" === t.insertAt) r ? r.nextSibling ? n.insertBefore(e, r.nextSibling) : n.appendChild(e) : n.insertBefore(e, n.firstChild), g.push(e); else {
                if ("bottom" !== t.insertAt) throw new Error("Invalid value for parameter 'insertAt'. Must be 'top' or 'bottom'.");
                n.appendChild(e)
            }
        }

        function a(t) {
            t.parentNode.removeChild(t);
            var e = g.indexOf(t);
            e >= 0 && g.splice(e, 1)
        }

        function s(t) {
            var e = document.createElement("style");
            return e.type = "text/css", i(t, e), e
        }

        function u(t, e) {
            var n, r, o;
            if (e.singleton) {
                var i = v++;
                n = b || (b = s(e)), r = l.bind(null, n, i, !1), o = l.bind(null, n, i, !0)
            } else n = s(e), r = c.bind(null, n), o = function () {
                a(n)
            };
            return r(t), function (e) {
                if (e) {
                    if (e.css === t.css && e.media === t.media && e.sourceMap === t.sourceMap) return;
                    r(t = e)
                } else o()
            }
        }

        function l(t, e, n, r) {
            var o = n ? "" : r.css;
            if (t.styleSheet) t.styleSheet.cssText = y(e, o); else {
                var i = document.createTextNode(o), a = t.childNodes;
                a[e] && t.removeChild(a[e]), a.length ? t.insertBefore(i, a[e]) : t.appendChild(i)
            }
        }

        function c(t, e) {
            var n = e.css, r = e.media, o = e.sourceMap;
            if (r && t.setAttribute("media", r), o && (n += "\n/*# sourceURL=" + o.sources[0] + " */", n += "\n/*# sourceMappingURL=data:application/json;base64," + btoa(unescape(encodeURIComponent(JSON.stringify(o)))) + " */"), t.styleSheet) t.styleSheet.cssText = n; else {
                for (; t.firstChild;) t.removeChild(t.firstChild);
                t.appendChild(document.createTextNode(n))
            }
        }

        var f = {}, p = function (t) {
            var e;
            return function () {
                return "undefined" == typeof e && (e = t.apply(this, arguments)), e
            }
        }, d = p(function () {
            return /msie [6-9]\b/.test(window.navigator.userAgent.toLowerCase())
        }), h = p(function () {
            return document.head || document.getElementsByTagName("head")[0]
        }), b = null, v = 0, g = [];
        t.exports = function (t, e) {
            e = e || {}, "undefined" == typeof e.singleton && (e.singleton = d()), "undefined" == typeof e.insertAt && (e.insertAt = "bottom");
            var n = o(t);
            return r(n, e), function (t) {
                for (var i = [], a = 0; a < n.length; a++) {
                    var s = n[a], u = f[s.id];
                    u.refs--, i.push(u)
                }
                if (t) {
                    var l = o(t);
                    r(l, e)
                }
                for (var a = 0; a < i.length; a++) {
                    var u = i[a];
                    if (0 === u.refs) {
                        for (var c = 0; c < u.parts.length; c++) u.parts[c]();
                        delete f[u.id]
                    }
                }
            }
        };
        var y = function () {
            var t = [];
            return function (e, n) {
                return t[e] = n, t.filter(Boolean).join("\n")
            }
        }()
    }, function (t, e, n) {
        var r = n(81);
        "string" == typeof r && (r = [[t.id, r, ""]]);
        n(86)(r, {});
        r.locals && (t.exports = r.locals)
    }])
});
//# sourceMappingURL=vue-select.js.map