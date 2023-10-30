-- NUMBER: 1.15

CREATE TABLE IF NOT EXISTS "public"."PaymentPlanDetails"
(
    "PaymentPlanDetailId"   BIGSERIAL      PRIMARY KEY,
    "PaymentPlanId"         BIGINT         NOT NULL,
    "PaymentDate"           TIMESTAMP      NOT NULL,
    "PaymentValue"          DECIMAL(22, 4) NOT NULL,
    "Balance"               DECIMAL(22, 4) NOT NULL,
    "CreatedDate"           TIMESTAMP      NOT NULL DEFAULT CURRENT_TIMESTAMP,
    "ModifiedDate"          TIMESTAMP      NULL,
    "DeletedDate"           TIMESTAMP      NULL,
    "IsEdit"                BOOLEAN        NOT NULL DEFAULT FALSE,
    "IsDelete"              BOOLEAN        NOT NULL DEFAULT FALSE,
    CONSTRAINT "FK_PaymentPlanDetails_PaymentPlanId_PaymentPlan_PaymentPlanId" FOREIGN KEY ("PaymentPlanId") REFERENCES "public"."PaymentPlan"("PaymentPlanId")
);
