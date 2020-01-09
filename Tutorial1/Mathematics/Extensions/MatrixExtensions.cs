using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

using MathNet.Numerics.LinearAlgebra;
using MathNet.Spatial.Euclidean;

using GFX.Tutorial.Engine.Common;

namespace GFX.Tutorial.Mathematics.Extensions
{
    public static class MatrixExtensions
    {

        #region // import

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix<double> ToMatrix(this double[] values, int rows, int columns)
        {
            return Matrix<double>.Build.DenseOfRowMajor(rows, columns, values);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix<double> ToMatrix(this double[] values)
        {
            return ToMatrix(values, 4, 4);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix<double> ToMatrix(this float[] values)
        {
            return ToMatrix(values.Select(floatValue => (double)floatValue).ToArray());
        }

        #endregion

        #region // export

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double[] ToDoublesRowMajor(this Matrix<double> matrix)
        {
            return matrix.AsRowMajorArray() ?? matrix.ToRowMajorArray();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double[] ToDoublesColumnMajor(this Matrix<double> matrix)
        {
            return matrix.AsColumnMajorArray() ?? matrix.ToColumnMajorArray();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double[] ToDoubles(this Matrix<double> matrix)
        {
            return matrix.ToDoublesRowMajor();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float[] ToFloatsRowMajor(this Matrix<double> matrix)
        {
            return matrix.ToDoublesRowMajor().Select(d => (float)d).ToArray();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float[] ToFloatsColunMajor(this Matrix<double> matrix)
        {
            return matrix.ToDoublesColumnMajor().Select(d => (float)d).ToArray();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float[] ToFloats(this Matrix<double> matrix)
        {
            return matrix.ToDoubles().Select(d => (float)d).ToArray();
        }

        #endregion

        #region // identity

        private static Matrix<double> s_Identity { get; } =
            new double[]
            {
                1, 0, 0, 0,
                0, 1, 0, 0,
                0, 0, 1, 0,
                0, 0, 0, 1
            }.ToMatrix();

        /// <summary>
        /// clones the static matrix which is faster than creating a new one every time
        /// </summary>
        public static Matrix<double> Identity => s_Identity.Clone();

        #endregion

        #region // transform

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void MultiplyRowMajor(this Matrix<double> m, double x, double y, double z, double w,
            out double _x, out double _y, out double _z, out double _w)
        {
            _x = m[0, 0] * x + m[1, 0] * y + m[2, 0] * z + m[3, 0] * w;
            _y = m[0, 1] * x + m[1, 1] * y + m[2, 1] * z + m[3, 1] * w;
            _z = m[0, 2] * x + m[1, 2] * y + m[2, 2] * z + m[3, 2] * w;
            _w = m[0, 3] * x + m[1, 3] * y + m[2, 3] * z + m[3, 3] * w;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void MultiplyColumnMajor(this Matrix<double> m, double x, double y, double z, double w,
            out double _x, out double _y, out double _z, out double _w)
        {
            _x = m[0, 0] * x + m[0, 1] * y + m[0, 2] * z + m[0, 3] * w;
            _y = m[1, 0] * x + m[1, 1] * y + m[1, 2] * z + m[1, 3] * w;
            _z = m[2, 0] * x + m[2, 1] * y + m[2, 2] * z + m[2, 3] * w;
            _w = m[3, 0] * x + m[3, 1] * y + m[3, 2] * z + m[3, 3] * w;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point3D Transform(this Matrix<double> m, in Point3D v)
        {
            MultiplyRowMajor(m, v.X, v.Y, v.Z, 1, out double x, out double y, out double z, out double w);
            return new Point3D(x / w, y / w, z / w);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3D Transform(this Matrix<double> m, in Vector3D v)
        {
            MultiplyRowMajor(m, v.X, v.Y, v.Z, 1, out double x, out double y, out double z, out double w);
            return new Vector3D(x / w, y / w, z / w);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3D Transform(this Matrix<double> m, in UnitVector3D v)
        {
            MultiplyRowMajor(m, v.X, v.Y, v.Z, 1, out double x, out double y, out double z, out double w);
            return new Vector3D(x / w, y / w, z / w);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Point3D> Transform(this Matrix<double> matrix, IEnumerable<Point3D> value)
        {
            IEnumerable<Point3D> matrixToReturn = value.Select(v => matrix.Transform(v));
            return matrixToReturn;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Vector3D> Transform(this Matrix<double> matrix, IEnumerable<Vector3D> value)
        {
            IEnumerable<Vector3D> matrixToReturn = value.Select(v => matrix.Transform(v));
            return matrixToReturn;
        }

        public static void Transform(this Matrix<double> matrix, ref Point3D[] value)
        {
            for (int i = 0; i < value.Length; i++)
            {
                // ref : references the pointer to the index so that the value
                // does not need to be accessed twice to get the value and set the value
                ref Point3D reference = ref value[i];
                reference = matrix.Transform(reference);
            }
        }

        public static void Transform(this Matrix<double> matrix, ref Vector3D[] value)
        {
            for (int i = 0; i < value.Length; i++)
            {
                ref Vector3D reference = ref value[i];
                reference = matrix.Transform(reference);
            }
        }

        #endregion

        #region // transformations

        #region // scale

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix<double> Scale(double x, double y, double z)
        {
            return new[]
            {
                x, 0, 0, 0,
                0, y, 0, 0,
                0, 0, z, 0,
                0, 0, 0, 1
            }.ToMatrix();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix<double> Scale(double uniform)
        {
            return Scale(uniform, uniform, uniform);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix<double> Scale(in Point3D value)
            // in keyword makes the variable "value" readonly so it can not be changed
        {
            return Scale(value.X, value.Y, value.Z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix<double> Scale(in Vector3D value)
        {
            return Scale(value.X, value.Y, value.Z);
        }


        #endregion

        #region // rotate

        public static Matrix<double> Rotate(in UnitVector3D axis, double angle)
        {
            var x = axis.X;
            var y = axis.Y;
            var z = axis.Z;
            var cosAngle = Math.Cos(angle);
            var sinAngle = Math.Sin(angle);
            var xx = x * x;
            var yy = y * y;
            var zz = z * z;
            var xy = x * y;
            var xz = x * z;
            var yz = y * z;

            return new[]
            {
                xx + cosAngle * (1 - xx),
                xy - cosAngle * xy + sinAngle * z,
                xz - cosAngle * xz - sinAngle * y,
                0,
                xy - cosAngle * xy - sinAngle * z,
                yy + cosAngle * (1 - yy),
                yz - cosAngle * yz + sinAngle * x,
                0,
                xz - cosAngle * xz + sinAngle * y,
                yz - cosAngle * yz - sinAngle * x,
                zz + cosAngle * (1 - zz),
                0,
                0, 0, 0, 1
            }.ToMatrix();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix<double> Rotate(in Vector3D axis, double angle)
        {
            return Rotate(axis.Normalize(), angle);
        }

        public static Matrix<double> Rotate(in Quaternion rotation)
        {
            double xx = rotation.ImagX * rotation.ImagX;
            double yy = rotation.ImagY * rotation.ImagY;
            double zz = rotation.ImagZ * rotation.ImagZ;

            double xy = rotation.ImagX * rotation.ImagY;
            double xz = rotation.ImagX * rotation.ImagZ;
            double yz = rotation.ImagY * rotation.ImagZ;

            double xw = rotation.ImagX * rotation.Real;
            double yw = rotation.ImagY * rotation.Real;
            double zw = rotation.ImagZ * rotation.Real;

            return new[]
            {
                1 - 2 * (yy + zz),
                2 * (xy + zw),
                2 * (xz - yw),
                0,
                2 * (xy - zw),
                1 - 2 * (zz + xx),
                2 * (yz + xw),
                0,
                2 * (xz + yw),
                2 * (yz - xw),
                1 - 2 * (yy + xx),
                0,
                0, 0, 0, 1
            }.ToMatrix();
        }

        public static Quaternion ToQuaternion(this Matrix<double> matrix)
        {
            double x, y, z, w;

            double num1 = matrix[0, 0] + matrix[1, 1] + matrix[2, 2];

            if (num1 > 0.0)
            {
                double num2 = Math.Sqrt(num1 + 1);
                double num3 = 0.5 / num2;
                x = (matrix[1, 2] - matrix[2, 1]) * num3;
                y = (matrix[2, 0] - matrix[0, 2]) * num3;
                z = (matrix[0, 1] - matrix[1, 0]) * num3;
                w = num2 * 0.5;
            }
            else if (matrix[0, 0] >= matrix[1, 1] && matrix[0, 0] >= matrix[2, 2])
            {
                double num2 = Math.Sqrt(1.0 + matrix[0, 0] - matrix[1, 1] - matrix[2, 2]);
                double num3 = 0.5 / num2;
                x = 0.5 * num2;
                y = (matrix[0, 1] + matrix[1, 0]) * num3;
                z = (matrix[0, 2] + matrix[2, 0]) * num3;
                w = (matrix[1, 2] - matrix[2, 1]) * num3;
            }
            else if (matrix[1, 1] >= matrix[2, 2])
            {
                double num2 = Math.Sqrt(1.0 + matrix[1, 1] - matrix[0, 0] - matrix[2, 2]);
                double num3 = 0.5 / num2;
                x = (matrix[1, 0] + matrix[0, 1]) * num3;
                y = num2 * 0.5;
                z = (matrix[2, 1] + matrix[1, 2]) * num3;
                w = (matrix[2, 0] - matrix[0, 2]) * num3;
            }
            else
            {
                double num2 = Math.Sqrt(1.0 + matrix[2, 2] - matrix[0, 0] - matrix[1, 1]);
                double num3 = 0.5 / num2;
                x = (matrix[2, 0] + matrix[0, 2]) * num3;
                y = (matrix[2, 1] + matrix[1, 2]) * num3;
                z = num2 * 0.5;
                w = (matrix[0, 1] - matrix[1, 0]) * num3;
            }

            return new Quaternion(w, x, y, z);
        }

        #endregion

        #region // translate

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix<double> Translate(double x, double y, double z)
        {
            return new[]
            {
                1, 0, 0, 0,
                0, 1, 0, 0,
                0, 0, 1, 0,
                x, y, z, 1
            }.ToMatrix();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix<double> Translate(in Point3D value)
        {
            return Translate(value.X, value.Y, value.Z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix<double> Translate(in Vector3D value)
        {
            return Translate(value.X, value.Y, value.Z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix<double> TransformAround(this Matrix<double> transformation, in Point3D transformationOrigin)
        {
            Matrix<double> translate = Translate(transformationOrigin);
            return translate.Inverse() * transformation * translate;
        }


        #endregion


        #endregion

        #region // graphics

        public static Matrix<double> LookAtRH(in Vector3D cameraPosition, in Vector3D cameraTarget, in UnitVector3D cameraUpVector)
        {
            UnitVector3D zAxis = (cameraPosition - cameraTarget).Normalize();
            UnitVector3D xAxis = cameraUpVector.CrossProduct(zAxis);
            UnitVector3D yAxis = zAxis.CrossProduct(xAxis);

            return new[]
            {
                xAxis.X, yAxis.X, zAxis.X, 0,
                xAxis.Y, yAxis.Y, zAxis.Y, 0,
                xAxis.Z, yAxis.Z, zAxis.Z, 0,
                -xAxis.DotProduct(cameraPosition), -yAxis.DotProduct(cameraPosition), -zAxis.DotProduct(cameraPosition), 1
            }.ToMatrix();
        }

        public static Matrix<double> PerspectiveForRH(double fieldOfView, double aspectRatio, double zNearPlane, double zFarPlane)
        {
            var h = 1 / Math.Tan(fieldOfView * 0.5);
            var w = h / aspectRatio;
            var zId = zFarPlane / (zNearPlane - zFarPlane);
            var wId = zNearPlane * zFarPlane / (zNearPlane - zFarPlane);

            return new[]
            {
                w, 0, 0, 0,
                0, h, 0, 0,
                0, 0, zId, -1,
                0, 0, wId, 0
            }.ToMatrix();
        }

        public static Matrix<double> OrthoRH(double width, double height, double zNearPlane, double zFarPlane)
        {
            var xId = 2 / width;
            var yId = 2 / height;
            var zId = 1 / (zNearPlane - zFarPlane);
            var wId = zNearPlane / (zNearPlane - zFarPlane);
            return new[]
            {
                xId, 0, 0, 0,
                0, yId, 0, 0,
                0, 0, zId, 0,
                0, 0, wId, 1
            }.ToMatrix();
        }

        public static Matrix<double> Viewport(in Viewport viewport)
        {
            var xId = viewport.Width * 0.5;
            var yId = -viewport.Height * 0.5;
            var zId = viewport.MaximumZ - viewport.MinimumZ;
            var wa = viewport.X + viewport.Width * 0.5;
            var wb = viewport.Y + viewport.Height * 0.5;
            var wId = viewport.MinimumZ;
            return new[]
            {
                xId, 0, 0, 0,
                0, yId, 0, 0,
                0, 0, zId, 0,
                wa, wb, wId, 1
            }.ToMatrix();
        }

        #endregion
    }
}
