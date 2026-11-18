;; ===== 1. try / catch / finally with three exception types =====
(println "=== 1. TRY / CATCH / FINALLY ===")

;; ArithmeticException
(try
  (println "Dividing 10 by 0...")
  (/ 10 0)
  (catch ArithmeticException e
    (println "Caught ArithmeticException:" (.getMessage e)))
  (finally
    (println "Finally: arithmetic test done\n")))

;; NullPointerException
(try
  (println "Calling .length on nil...")
  (.length nil)
  (catch NullPointerException e
    (println "Caught NullPointerException:" (.getMessage e)))
  (finally
    (println "Finally: null test done\n")))

;; Custom exception using ex-info
(try
  (println "Throwing a custom ex-info exception...")
  (throw (ex-info "Something custom went wrong" {:code 500 :reason "demo"}))
  (catch clojure.lang.ExceptionInfo e
    (println "Caught ExceptionInfo:" (ex-message e)))
  (finally
    (println "Finally: custom exception test done\n")))

;; ===== 2. Function that deliberately throws a custom exception =====
(println "=== 2. FUNCTION THAT THROWS ex-info ===")

(defn withdraw [balance amount]
  (cond
    (neg? amount)
    (throw (ex-info "Invalid amount: cannot be negative"
                    {:type :invalid-amount :amount amount}))

    (> amount balance)
    (throw (ex-info "Insufficient funds"
                    {:type :insufficient-funds
                     :balance balance
                     :requested amount
                     :shortfall (- amount balance)}))

    :else (- balance amount)))

(println "Withdraw 30 from 100:" (withdraw 100 30))

;; ===== 3. Graceful recovery with ex-message and ex-data =====
(println "\n=== 3. GRACEFUL RECOVERY (ex-message / ex-data) ===")

(defn safe-withdraw [balance amount]
  (try
    (withdraw balance amount)
    (catch clojure.lang.ExceptionInfo e
      (let [data (ex-data e)]
        (println "Error message:" (ex-message e))
        (println "Error data:   " data)
        (case (:type data)
          :insufficient-funds
          (do (println "Recovery: you are short by" (:shortfall data)
                       "- withdrawing the full balance instead.")
              0)

          :invalid-amount
          (do (println "Recovery: ignoring invalid amount, balance unchanged.")
              balance)

          (do (println "Recovery: unknown error, balance unchanged.")
              balance))))
    (finally
      (println "Finally: transaction attempt finished\n"))))

(println "Result:" (safe-withdraw 100 150))
(println "Result:" (safe-withdraw 100 -20))
(println "Result:" (safe-withdraw 100 40))