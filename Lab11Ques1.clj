;; ===== 1. STATIC METHODS (dot notation) =====
(println "=== STATIC METHODS ===")
(println "Math/abs -25:" (Math/abs -25))
(println "Math/sqrt 144:" (Math/sqrt 144))
(println "Math/max 10 42:" (Math/max 10 42))
(println "Math/PI (static field):" Math/PI)
(println "Integer/parseInt \"123\":" (Integer/parseInt "123"))
(println "System/currentTimeMillis:" (System/currentTimeMillis))
;; the same static call written with the dot special form
(println "(. Math abs -7):" (. Math abs -7))

;; ===== 2. CREATING JAVA OBJECTS =====
(println "\n=== CREATING JAVA OBJECTS ===")
(def jlist (new java.util.ArrayList))   ; (new ClassName) syntax
(def jmap  (java.util.HashMap.))        ; ClassName. syntax
(def sb    (StringBuilder. "Hello"))
(println "ArrayList created:" jlist (class jlist))
(println "HashMap created:" jmap (class jmap))

;; ===== 3. INSTANCE METHODS =====
(println "\n=== INSTANCE METHODS ===")
;; ArrayList
(.add jlist "Clojure")
(.add jlist "Java")
(.add jlist "Interop")
(println "ArrayList after add:" jlist)
(println "size:" (.size jlist))
(println "get index 1:" (.get jlist 1))
(println "contains Java?:" (.contains jlist "Java"))

;; HashMap
(.put jmap "a" 1)
(.put jmap "b" 2)
(println "HashMap after put:" jmap)
(println "get \"a\":" (.get jmap "a"))
(println "containsKey \"b\":" (.containsKey jmap "b"))

;; StringBuilder
(.append sb " World")
(println "StringBuilder:" (.toString sb))
(println "String methods:" (.toUpperCase "clojure") (.length "clojure"))

;; ===== 4. CONVERT CLOJURE <-> JAVA COLLECTIONS =====
(println "\n=== COLLECTION CONVERSION ===")
;; Clojure -> Java
(def cvec [1 2 3 4])
(def cmap {:x 10 :y 20})
(def java-list (java.util.ArrayList. cvec))
(def java-map  (java.util.HashMap. cmap))
(println "Clojure vector:" cvec)
(println "-> Java ArrayList:" java-list (class java-list))
(println "Clojure map:" cmap)
(println "-> Java HashMap:" java-map (class java-map))

;; Java -> Clojure
(def back-to-vec  (vec jlist))
(def back-to-seq  (seq jlist))
(def back-to-map  (into {} jmap))
(println "Java list -> Clojure vector:" back-to-vec (class back-to-vec))
(println "Java list -> Clojure seq:" back-to-seq)
(println "Java map -> Clojure map:" back-to-map (class back-to-map))

;; Clojure functions work directly on Java collections
(println "map count over Java list:" (map count jlist))