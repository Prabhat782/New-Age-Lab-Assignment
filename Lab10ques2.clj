;; Two different collections
(def numbers [1 2 3 4 5 6 7 8 9 10])
(def scores  '(45 82 67 90 38 71 55))

;; ===== 1. MAP (transform) =====
(println "=== MAP ===")
(println "Numbers squared:" (map #(* % %) numbers))
(println "Scores + 5 bonus:" (map #(+ % 5) scores))

;; ===== 2. FILTER (select by predicate) =====
(println "\n=== FILTER ===")
(println "Even numbers:" (filter even? numbers))
(println "Scores >= 60 (pass):" (filter #(>= % 60) scores))

;; ===== 3. REDUCE (aggregate) =====
(println "\n=== REDUCE ===")
(println "Sum of numbers:" (reduce + numbers))
(println "Max of numbers:" (reduce max numbers))
(println "Sum of scores:" (reduce + scores))
(println "Max score:" (reduce max scores))

;; ===== 4. TAKE and DROP (slicing) =====
(println "\n=== TAKE / DROP ===")
(println "take 3 numbers:" (take 3 numbers))
(println "drop 3 numbers:" (drop 3 numbers))
(println "take 2 scores:" (take 2 scores))
(println "drop 2 scores:" (drop 2 scores))

;; ===== 5. THREADING MACROS =====
(println "\n=== THREADING MACROS (->>) ===")
;; numbers: keep evens, square them, sum them
(println "Numbers: even -> square -> sum:"
         (->> numbers
              (filter even?)
              (map #(* % %))
              (reduce +)))

;; scores: keep passing, add 5, take first 3
(println "Scores: pass -> +5 -> take 3:"
         (->> scores
              (filter #(>= % 60))
              (map #(+ % 5))
              (take 3)))

(println "\n=== THREADING MACRO (->) ===")
;; -> puts the value as the FIRST argument
(println "Using ->: 10 -> +5 -> *2 -> -3:"
         (-> 10
             (+ 5)
             (* 2)
             (- 3)))